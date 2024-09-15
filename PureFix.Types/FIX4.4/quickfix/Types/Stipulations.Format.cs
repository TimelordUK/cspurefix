using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class Stipulations
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			if (NoStipulations != null)
			{
				for (var i = 0; i < NoStipulations.Length; ++i)
				{
					((IFixEncoder)NoStipulations[i])?.Encode(storage, tags, delimiter);
				}
			}
		}
	}
}
