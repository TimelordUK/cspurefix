using PureFix.MessageStore;
using PureFix.MessageStore.TextFileStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.MessageStore
{
    public abstract class CommonStoreTestsBase
    {
        protected const string LogonText = "8=FIX4.4|9=0000208|35=A|49=sender-10|56=target-20|34=1|57=sub-a|52=20180610-10:39:01.621|98=2|108=62441|95=20|96=VgfoSqo56NqSVI1fLdlI|141=Y|789=4886|383=20|384=1|372=ipsum|385=R|464=N|553=sit|554=consectetur|10=49|";
        protected const string HeartbeatText = "8=FIX.4.4|9=0000104|35=0|49=init-comp|56=accept-comp|34=2|57=fix|52=20230101-14:14:20.930|112=Sun, 01 Jan 2023 14:14:20 GMT|10=029|";


        protected async Task DoMessageWrittenToFile(Func<IMessageStore> factory)
        {
            using(var store = factory())
            {
                await store.Initialize();
                await store.Store(ToBuffer(LogonText));                
            }
        }

        /// <summary>
        /// This tests expectd there to be 3 messages in the store with sequence numbers, 1, 2 and 3
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        protected async Task DoHasMessages(Func<IMessageStore> factory)
        {
            using(var store = factory())
            {
                await store.Initialize();
            
                var found = await store.TryGetMessage(1, out var logonbeatMessage);
                Assert.That(found, Is.True);
                Assert.That(logonbeatMessage.Length, Is.GreaterThan(0));

                found = await store.TryGetMessage(2, out var heartbeatMessage);
                Assert.That(found, Is.True);
                Assert.That(heartbeatMessage.Length, Is.GreaterThan(0));

                found = await store.TryGetMessage(3, out var otherMessage);
                Assert.That(found, Is.False);
                Assert.That(otherMessage.Length, Is.EqualTo(0));
            }
        }

        protected async Task DoStoreInRealFile(Func<IMessageStore> factory)
        {
            using(var store = factory())
            {
                await store.Initialize();
            
                await store.Store(ToBuffer(LogonText));
                await store.Store(ToBuffer(HeartbeatText));
            }
        }

        protected async Task DoStoreAndReloadInRealFile(Func<IMessageStore> factory)
        {
            using(var writeStore = factory())
            {
                await writeStore.Initialize();
            
                await writeStore.Store(ToBuffer(LogonText));
                await writeStore.Store(ToBuffer(HeartbeatText));
            }

            using(var readStore = factory())
            {
                await readStore.Initialize();
            
                var found = await readStore.TryGetMessage(1, out var logonbeatMessage);
                Assert.That(found, Is.True);
                Assert.That(logonbeatMessage.Length, Is.GreaterThan(0));

                found = await readStore.TryGetMessage(2, out var heartbeatMessage);
                Assert.That(found, Is.True);
                Assert.That(heartbeatMessage.Length, Is.GreaterThan(0));
            }
        }

        
        protected Memory<byte> ToBuffer(string line)
        {
            return line.Replace('|', (char)0x01).Select(c => (byte)c).ToArray();
        }

        protected async Task WithFile(Func<string, Task> function)
        {
            var filename = Path.GetTempFileName();
            Console.WriteLine($"temp file = {filename}");

            try
            {
                await function(filename);
            }
            finally
            {
                File.Delete(filename);
            }
        }    
    }
}
