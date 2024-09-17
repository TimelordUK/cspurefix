using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFIix.Test.Env;
using PureFix.Buffer.Ascii;
using PureFix.MessageStore;
using PureFix.MessageStore.TextFileStore;

namespace PureFIix.Test.MessageStore.FileStore
{
    [TestFixture]
    public class TextFileMessageStoreTests : CommonStoreTestsBase
    {
        public enum FactoryType
        {
            Regular,
            Concurrent
        }

        private static IMessageStore MakeFactory(FactoryType type, ITextFileAccess access, TextFileMessageStoreConfig config)
        {
            return type switch
            {
                FactoryType.Regular => new TextFileMessageStore(access, config),
                FactoryType.Concurrent => new ConcurrentTextFileMessageStore(access, config),
                _ => throw new Exception()
            };
        }

        [Test]
        [TestCase(FactoryType.Regular)]
        [TestCase(FactoryType.Concurrent)]
        public void Initialize(FactoryType type)
        {
            var fileAccess = new InMemoryTextFileAccess();
            var config = new TextFileMessageStoreConfig
            {
                Filename = "foo.dat"
            };

            using(var store = MakeFactory(type, fileAccess, config))
            {
                Assert.DoesNotThrowAsync(async () => await store.Initialize());
            }
        }

        [Test]
        [TestCase(FactoryType.Regular)]
        [TestCase(FactoryType.Concurrent)]
        public void CommentInFile(FactoryType type)
        {
            var fileAccess = new InMemoryTextFileAccess("#This line is invalid");
            var config = new TextFileMessageStoreConfig
            {
                Filename = "foo.dat"
            };

            using(var store = MakeFactory(type, fileAccess, config))
            {
                Assert.DoesNotThrowAsync(async () => await store.Initialize());
            }
        }

        [Test]
        [TestCase(FactoryType.Regular)]
        [TestCase(FactoryType.Concurrent)]
        public void EmptyLineInFile(FactoryType type)
        {
            var fileAccess = new InMemoryTextFileAccess("");
            var config = new TextFileMessageStoreConfig
            {
                Filename = "foo.dat"
            };

            using(var store = MakeFactory(type, fileAccess, config))
            {
                Assert.DoesNotThrowAsync(async () => await store.Initialize());
            }
        }

        [Test]
        [TestCase(FactoryType.Regular)]
        [TestCase(FactoryType.Concurrent)]
        public async Task TryGetMessage_Empty(FactoryType type)
        {
            var fileAccess = new InMemoryTextFileAccess();
            var config = new TextFileMessageStoreConfig
            {
                Filename = "foo.dat"
            };

            using(var store = MakeFactory(type, fileAccess, config))
            {
                await store.Initialize();

                var found = await store.TryGetMessage(1, out var message);
                Assert.That(found, Is.False);
            }
        }

        [Test]
        [TestCase(FactoryType.Regular)]
        [TestCase(FactoryType.Concurrent)]
        public async Task MessageWrittenToFile(FactoryType type)
        {
            var fileAccess = new InMemoryTextFileAccess();
            Assert.That(fileAccess.GetContents(), Has.Length.EqualTo(0));

            var config = new TextFileMessageStoreConfig
            {
                Filename = "foo.dat",
                SohReplacement = (byte)'|'
            };

            await DoMessageWrittenToFile(() => MakeFactory(type, fileAccess, config));
            Assert.That(fileAccess.GetContents(), Has.Length.GreaterThan(0));
        }

        [Test]
        [TestCase(FactoryType.Regular)]
        [TestCase(FactoryType.Concurrent)]
        public async Task HasMessages(FactoryType type)
        {
            var fileAccess = new InMemoryTextFileAccess(LogonText, HeartbeatText);
            var config = new TextFileMessageStoreConfig
            {
                Filename = "foo.dat",
                SohReplacement = (byte)'|'
            };

            await DoHasMessages(() => MakeFactory(type, fileAccess, config));
        }

        [Test]
        [TestCase(FactoryType.Regular)]
        [TestCase(FactoryType.Concurrent)]
        public async Task StoreInRealFile(FactoryType type)
        {
            await WithFile(async filename =>
            {
                var config = new TextFileMessageStoreConfig
                {
                    Filename = filename,
                    SohReplacement = (byte)'|'
                };

                var fileAccess = new FileSysytemTextFileAccess();
                await DoStoreInRealFile(() => MakeFactory(type, fileAccess, config));
            });
        }

        [Test]
        [TestCase(FactoryType.Regular)]
        [TestCase(FactoryType.Concurrent)]
        public async Task StoreAndReloadInRealFile(FactoryType type)
        {
            await WithFile(async filename =>
            {
                var config = new TextFileMessageStoreConfig
                {
                    Filename = filename,
                    SohReplacement = (byte)'|'
                };

                var fileAccess = new FileSysytemTextFileAccess();

                await DoStoreAndReloadInRealFile(() => MakeFactory(type, fileAccess, config));
            });
        }
    }
}
