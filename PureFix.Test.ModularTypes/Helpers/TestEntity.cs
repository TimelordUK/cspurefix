using PureFix.Buffer.Ascii;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using PureFix.Transport;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace PureFix.Test.ModularTypes.Helpers
{
    internal class TestEntity
    {
        public IFixDefinitions Definitions { get; }
        public AsciiParser Parser { get; private set; }
        public IFixClock Clock { get; private set; }

        public TestEntity(string dataDict = "FIX44.xml")
        {
            Clock = new TestClock();
            Definitions = new FixDefinitions();
            var qf = new QuickFixXmlFileParser(Definitions);
            qf.Parse(Path.Join(Fix44PathHelper.DataDictRootPath, dataDict));
            Prepare();
        }

        public void Prepare()
        {
            Parser = new AsciiParser(Definitions) { Delimiter = AsciiChars.Pipe, WriteDelimiter = AsciiChars.Pipe };
        }

        public async Task<List<AsciiView>> Replay(string path)
        {
            var views = new List<AsciiView>();

            // Read the file and parse FIX messages
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Replay file not found: {path}");
            }

            var content = await File.ReadAllTextAsync(path);
            var bytes = Encoding.UTF8.GetBytes(content);

            Parser.ParseFrom(bytes, bytes.Length, (i, view) => views.Add((AsciiView)view));

            return views;
        }

        public List<AsciiView> ParseText(string s)
        {
            var views = new List<AsciiView>(10000);
            var b = Encoding.UTF8.GetBytes(s);
            Parser.ParseFrom(b, b.Length, (i, view) => views.Add((AsciiView)view));
            return views;
        }

        public static async Task<string> GetTextAsync(string file)
        {
            using var streamReader = File.OpenText(file);
            var all = await streamReader.ReadToEndAsync();
            return all;
        }

        public static string GetText(string file)
        {
            using var streamReader = File.OpenText(file);
            var all = streamReader.ReadToEnd();
            return all;
        }

        public List<AsciiView> ParseTestHunks(string s)
        {
            var b = Encoding.UTF8.GetBytes(s);
            var span = new Span<byte>(b);
            var iteration = 0;
            var views = new List<AsciiView>();
            while (span.Length > 0)
            {
                var want = Math.Min(span.Length, iteration % 10 + 1);
                Parser.ParseFrom(span[..want], want, (i, view) => views.Add((AsciiView)view));
                span = span[want..];
                ++iteration;
            }
            return views;
        }

        public static async Task<Dictionary<string, int>> GetJsonDict(string file)
        {
            using var streamReader = File.OpenText(file);
            var json = await streamReader.ReadToEndAsync();
            var values = JsonSerializer.Deserialize<Dictionary<string, int>>(json);
            return values;
        }

        public void TimeParsePath(string description, string path, int repeats, int batch = 1)
        {
            var sw = new System.Diagnostics.Stopwatch();
            var one = GetText(path);
            var all = string.Concat(Enumerable.Repeat(one, repeats));
            var count = repeats * batch;
            var b = Encoding.UTF8.GetBytes(all);
            var msgs = new List<AsciiView>(count);

            sw.Start();
            Parser.ParseFrom(b, b.Length, Action);
            sw.Stop();

            Console.WriteLine($"{description}[{count},{batch}]: {sw.Elapsed.TotalMilliseconds} {(decimal)sw.Elapsed.TotalMicroseconds / count}  micro/msg {Parser.ParserStats.TotalSegmentParseMicro / count} seg/msg {Parser.ParserStats}");
            return;

            void Action(int i, MsgView view) => msgs.Add((AsciiView)view);
        }

        public IFixConfig GetTestInitiatorConfig(string json = "test-qf44-initiator.json")
        {
            return GetConfig(json);
        }

        public IFixConfig GetTestInitiator52Config(string json = "test-qf52-initiator.json")
        {
            return GetConfig(json);
        }

        public IFixConfig GetTestAcceptorConfig(string json = "test-qf44-acceptor.json")
        {
            return GetConfig(json);
        }

        public IFixConfig GetTestAcceptor52Config(string json = "test-qf52-acceptor.json")
        {
            return GetConfig(json);
        }

        public IFixConfig GetConfig(string json)
        {
            var config = FixConfig.MakeConfigFromPaths(Fix44PathHelper.DataDictRootPath, Path.Join(Fix44PathHelper.SessionRootPath, json));
            config.LogDelimiter = AsciiChars.Pipe;

            // Cast to FixConfig to access the setter and use the new modular type system's SessionMessageFactory
            if (config is FixConfig { Description: not null } fixConfig)
            {
                fixConfig.MessageFactory = new Fix44ModularSessionMessageFactory(fixConfig.Description);
            }

            return config;
        }

        public async Task<IFixSessionStore> MakeMsgStore(IReadOnlyList<AsciiView> views, string? filter = null)
        {
            // Session messages to filter out (for backward compatibility with tests)
            var sessionMessages = new HashSet<string>
            {
                MsgType.Logon, MsgType.Logout, MsgType.ResendRequest,
                MsgType.Heartbeat, MsgType.TestRequest, MsgType.SequenceReset
            };
            filter ??= "accept-comp";
            var sessionId = new SessionId("FIX.4.4", filter, "target");
            var store = new MemorySessionStore(sessionId);
            await store.Initialize();
            foreach (var view in views)
            {
                if (view.SenderCompID() != filter) continue;
                var msgType = view.MsgType();
                // Filter out session messages for test compatibility
                if (msgType != null && sessionMessages.Contains(msgType))
                {
                    continue;
                }
                await store.Put(FixMsgStoreRecord.ToMsgStoreRecord(view));
            }
            return store;
        }
    }
}
