using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    public interface IFixMessageFactory
    {
        public bool TryParse(IMessageView view, [NotNullWhen(true)] out IFixMessage? message);

        /// <summary>
        /// Creates a FIX message and populates it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="view"></param>
        /// <returns></returns>
        public T MakeAndParse<T>(IMessageView view) where T : IFixMessage, new()
        {
            var message = new T();
            message.Parse(view);

            return message;
        }
    }
}
