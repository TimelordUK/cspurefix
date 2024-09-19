using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFIix.Test.Env;

namespace PureFIix.Test.Ascii
{
    internal class SessionTest
    {
        [Test]
        public async Task Test_Transport_Test()
        {
            var lhs = new TestMessageTransport();
            var rhs = new TestMessageTransport();
            lhs.ConnectTo(rhs);
            rhs.ConnectTo(lhs);
            const string s = "hello world";
            var bytes = Encoding.UTF8.GetBytes(s);
            var cts = new CancellationTokenSource();
            await lhs.SendAsync(bytes, cts.Token);
            var buffer = new byte[1024];
            var received = await rhs.ReceiveAsync(buffer, cts.Token);
            var str = Encoding.Default.GetString(buffer,0,received);
            Assert.That(str, Is.EqualTo(s));

            await rhs.SendAsync(bytes, cts.Token);
            received = await lhs.ReceiveAsync(buffer, cts.Token);
            Assert.That(received, Is.EqualTo(s.Length));
            Assert.That(Encoding.Default.GetString(buffer, 0, received), Is.EqualTo(s));
        }

        [Test]
        public void Test_Logging_Test()
        {
            var clock = new TestClock();
            var factory = new TestLoggerFactory(clock);
            var myLog = factory.MakeLogger("initiator");
            myLog.Info("initiating logon");
            var logger = (TestLogger)myLog;
            var entries = logger.Entries;
            Assert.That(entries, Has.Count.EqualTo(1));
        }
    }
}
