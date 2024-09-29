using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    /*
    ***************************************************************
    * Code to identify reason for a session-level Reject message. *
    ***************************************************************
    */
    public enum SessionRejectReason
    {
        InvalidTagNumber = 0,
        RequiredTagMissing = 1,
        TagNotDefinedForThisMessageType = 2,
        UndefinedTag = 3,
        TagSpecifiedWithoutAValue = 4,
        ValueIsIncorrect = 5,
        IncorrectDataFormatForValue = 6,
        DecryptionProblem = 7,
        SignatureProblem = 8,
        CompIdProblem = 9,
        SendingTimeAccuracyProblem = 10,
        InvalidMsgType = 11,
        XmlValidationError = 12,
        TagAppearsMoreThanOnce = 13,
        TagSpecifiedOutOfRequiredOrder = 14,
        RepeatingGroupFieldsOutOfOrder = 15,
        IncorrectNumInGroupCountForRepeatingGroup = 16,
        Non = 17,
        Invalid = 18,
        Other = 99
    }
}
