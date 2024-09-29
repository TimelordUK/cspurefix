using Arrow.Threading.Tasks;
using PureFix.Buffer;
using PureFix.Dictionary.Definition;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Transport.Session
{
    public interface ISessionFactory
    {
        FixSession MakeSession();
    }
}
