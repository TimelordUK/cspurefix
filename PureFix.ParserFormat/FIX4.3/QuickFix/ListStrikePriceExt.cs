using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("m", FixVersion.FIX43)]
	public static class ListStrikePriceExt
	{
		public static void Parse(this ListStrikePrice instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.ListID = view.GetString(66);
			instance.TotNoStrikes = view.GetInt32(422);
			var groupViewNoStrikes = view.GetView("NoStrikes");
			if (groupViewNoStrikes is null) return;
			
			var countNoStrikes = groupViewNoStrikes.GroupCount();
			instance.NoStrikes = new ListStrikePriceNoStrikes[countNoStrikes];
			for (var i = 0; i < countNoStrikes; ++i)
			{
				instance.NoStrikes[i] = new();
				instance.NoStrikes[i].Parse(groupViewNoStrikes[i]);
			}
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
