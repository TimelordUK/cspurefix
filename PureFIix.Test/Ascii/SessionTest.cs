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
            var s = "hello world";
            var bytes = Encoding.UTF8.GetBytes(s);
            var cts = new CancellationTokenSource();
            await lhs.SendAsync(bytes, cts.Token);
            var buffer = new byte[1024];
            var received = await rhs.ReceiveAsync(buffer, cts.Token);
            var str = System.Text.Encoding.Default.GetString(buffer,0,received);
            Assert.That(str, Is.EqualTo(s));

        }
    }
}
