using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("u", FixVersion.FIX43)]
	public static class CrossOrderCancelRequestExt
	{
		public static void Parse(this CrossOrderCancelRequest instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.OrderID = view.GetString(37);
			instance.CrossID = view.GetString(548);
			instance.OrigCrossID = view.GetString(551);
			instance.CrossType = view.GetInt32(549);
			instance.CrossPrioritization = view.GetInt32(550);
			var groupViewNoSides = view.GetView("NoSides");
			if (groupViewNoSides is null) return;
			
			var countNoSides = groupViewNoSides.GroupCount();
			instance.NoSides = new CrossOrderCancelRequestNoSides[countNoSides];
			for (var i = 0; i < countNoSides; ++i)
			{
				instance.NoSides[i] = new();
				instance.NoSides[i].Parse(groupViewNoSides[i]);
			}
			if (view.GetView("Instrument") is MsgView groupViewInstrument)
			{
				instance.Instrument = new Instrument();
				instance.Instrument!.Parse(groupViewInstrument);
			}
			instance.TransactTime = view.GetDateTime(60);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
