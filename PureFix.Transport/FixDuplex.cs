using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace PureFix.Transport
{
    public class FixDuplex<T>
    {
        private readonly Channel<T> _channel = Channel.CreateUnbounded<T>();
        public ChannelReader<T> Reader => _channel.Reader;
        public ChannelWriter<T> Writer => _channel.Writer;
    }
}
