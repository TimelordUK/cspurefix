using PureFix.Examples.Skeleton;
using PureFix.Test.ModularTypes.Helpers;
using PureFix.Examples.TradeCapture;
using PureFix.Test.ModularTypes.Helpers;
﻿using PureFix.Transport.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Transport;
using PureFix.Transport.Session;
using PureFix.Types;
using PureFix.Transport.Recovery;

namespace PureFix.Test.ModularTypes.Env.TradeCapture
{
    internal class TradeCaptureTestLogRecovery : FixLogRecovery
    {
        public string Path { get; set; } = Fix44PathHelper.TradeCaptureRecoveryPath;

        public TradeCaptureTestLogRecovery(IFixLogParser parser, ILogFactory logFactory, IFixConfig config, IFixMsgStore msgStore)
            : base(parser, logFactory, config, msgStore)
        {
        }

        protected override FileInfo GetFixFileInfo()
        {
            return new FileInfo(Path);
        }
    }
}
