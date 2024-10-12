using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arrow.Threading.Tasks;
using PureFix.Test.Env;
using PureFix.Test.Env.Experiment;
using PureFix.Test.Env.Skeleton;
using PureFix.Test.Env.TradeCapture;
using PureFix.Transport;
using PureFix.Types.Config;

namespace PureFix.Test.Ascii
{
    internal partial class SessionTest
    {

        /*
           8=FIX.4.4|9=0000105|35=1|49=accept-comp|56=init-comp|34=127|57=fix|52=20241012-15:10:21.269|112=test-req-10/12/2024 15:10:21|10=204|
           8=FIX.4.4|9=000105|35=0|49=init-comp|56=accept-comp|34=127|57=fix|52=20241012-15:10:21.272|112=test-req-10/12/2024 15:10:21|10=149|
         */

        [Test]
        public async Task Recovery_Skeleton_File_Test()
        {
            var experiment = new SkeletonSessionExperiment(_testEntity);
            var initDescription = (SessionDescription)experiment.InitiatorConfig.Description;
            var acceptDescription = (SessionDescription)experiment.AcceptorConfig.Description;
            Assert.Multiple(() =>
            {
                Assert.That(initDescription, Is.Not.Null);
                Assert.That(acceptDescription, Is.Not.Null);
            });
            initDescription.ResetSeqNumFlag = false;
            acceptDescription.ResetSeqNumFlag = false;
            var recovery = experiment.Initiator.Recovery;
            Assert.That(recovery, Is.Not.Null);
            await recovery.Recover();
            Assert.That(recovery.MySeqNum, Is.EqualTo(127));
            Assert.That(recovery.PeerSeqNum, Is.EqualTo(127));
        }

        [Test]
        public async Task Recovery_TradeCapture_File_Test()
        {
            var experiment = new TradeCaptureSessionExperiment(_testEntity);
            var initDescription = (SessionDescription)experiment.InitiatorConfig.Description;
            var acceptDescription = (SessionDescription)experiment.AcceptorConfig.Description;
            Assert.Multiple(() =>
            {
                Assert.That(initDescription, Is.Not.Null);
                Assert.That(acceptDescription, Is.Not.Null);
            });
            initDescription.ResetSeqNumFlag = false;
            acceptDescription.ResetSeqNumFlag = false;
            var recovery = experiment.Initiator.Recovery;
            Assert.That(recovery, Is.Not.Null);
            await recovery.Recover();
            Assert.Multiple(() =>
            {
                Assert.That(recovery.MySeqNum, Is.EqualTo(5));
                Assert.That(recovery.PeerSeqNum, Is.EqualTo(122));
                Assert.That(recovery.LastStoreState, Is.Not.Null);
                // have sent request trade captures and logon (rest heartbeats are ignored).
                Assert.That(recovery.LastStoreState.Value.LastSeq, Is.EqualTo(2));
            });
        }

            [Test]
        public async Task Initiator_Acceptor_Recover_From_Log()
        {
            var experiment = new SkeletonSessionExperiment(_testEntity);
            var initDescription = (SessionDescription)experiment.InitiatorConfig.Description;
            var acceptDescription = (SessionDescription)experiment.AcceptorConfig.Description;
            Assert.Multiple(() =>
            {
                Assert.That(initDescription, Is.Not.Null);
                Assert.That(acceptDescription, Is.Not.Null);
            });
            initDescription.ResetSeqNumFlag = false;
            acceptDescription.ResetSeqNumFlag = false;
            await experiment.Run(experiment.OnReady, experiment.Initiator.App.Done);

            CheckLog(experiment);
            Assert.Multiple(() =>
            {
                Assert.That(experiment.Initiator.Encoder.MsgSeqNum, Is.GreaterThan(128));
                Assert.That(experiment.Acceptor.Encoder.MsgSeqNum, Is.GreaterThan(128));
            });
        }
    }
}
