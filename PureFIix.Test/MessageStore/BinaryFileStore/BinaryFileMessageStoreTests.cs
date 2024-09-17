using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PureFix.MessageStore;
using PureFix.MessageStore.BinaryFileStore;
using PureFix.MessageStore.TextFileStore;

namespace PureFIix.Test.MessageStore.BinaryFileStore
{
    public class BinaryFileMessageStoreTests : CommonStoreTestsBase
    {
        public enum FactoryType
        {
            Regular,
            Concurrent
        }

        private static IMessageStore MakeFactory(FactoryType type, IBinaryFileAccess access, BinaryFileMessageStoreConfig config)
        {
            return type switch
            {
                FactoryType.Regular => new BinaryFileMessageStore(access, config),
                _ => throw new Exception()
            };
        }

        [Test]
        [TestCase(FactoryType.Regular)]
        public void Initialize(FactoryType type)
        {
            var fileAccess = new InMemoryBinaryFileAccess();
            var config = new BinaryFileMessageStoreConfig
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
        public async Task TryGetMessage_Empty(FactoryType type)
        {
            var fileAccess = new InMemoryBinaryFileAccess();
            var config = new BinaryFileMessageStoreConfig
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
        public async Task HasMessages(FactoryType type)
        {
            var fileAccess = new InMemoryBinaryFileAccess(LogonText, HeartbeatText);
            var config = new BinaryFileMessageStoreConfig
            {
                Filename = "foo.dat",
            };

            await DoHasMessages(() => MakeFactory(type, fileAccess, config));
        }

        [Test]
        [TestCase(FactoryType.Regular)]
        public async Task StoreInRealFile(FactoryType type)
        {
            await WithFile(async filename =>
            {
                var config = new BinaryFileMessageStoreConfig
                {
                    Filename = filename,
                };

                var fileAccess = new FileSystemBinaryFileAccess();
                await DoStoreInRealFile(() => MakeFactory(type, fileAccess, config));
            });
        }

        [Test]
        [TestCase(FactoryType.Regular)]
        public async Task StoreAndReloadInRealFile(FactoryType type)
        {
            await WithFile(async filename =>
            {
                var config = new BinaryFileMessageStoreConfig
                {
                    Filename = filename,
                };

                var fileAccess = new FileSystemBinaryFileAccess();

                await DoStoreAndReloadInRealFile(() => MakeFactory(type, fileAccess, config));
            });
        }
    }
}
