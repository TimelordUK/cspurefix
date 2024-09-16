using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("6", FixVersion.FIX42)]
	public static class IOIExt
	{
		public static void Parse(this IOI instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.IOIid = view.GetString(23);
			instance.IOITransType = view.GetString(28);
			instance.IOIRefID = view.GetString(26);
			instance.Symbol = view.GetString(55);
			instance.SymbolSfx = view.GetString(65);
			instance.SecurityID = view.GetString(48);
			instance.IDSource = view.GetString(22);
			instance.SecurityType = view.GetString(167);
			instance.MaturityMonthYear = view.GetMonthYear(200);
			instance.MaturityDay = view.GetString(205);
			instance.PutOrCall = view.GetInt32(201);
			instance.StrikePrice = view.GetDouble(202);
			instance.OptAttribute = view.GetString(206);
			instance.ContractMultiplier = view.GetDouble(231);
			instance.CouponRate = view.GetDouble(223);
			instance.SecurityExchange = view.GetString(207);
			instance.Issuer = view.GetString(106);
			instance.EncodedIssuerLen = view.GetInt32(348);
			instance.EncodedIssuer = view.GetByteArray(349);
			instance.SecurityDesc = view.GetString(107);
			instance.EncodedSecurityDescLen = view.GetInt32(350);
			instance.EncodedSecurityDesc = view.GetByteArray(351);
			instance.Side = view.GetString(54);
			instance.IOIShares = view.GetString(27);
			instance.Price = view.GetDouble(44);
			instance.Currency = view.GetString(15);
			instance.ValidUntilTime = view.GetDateTime(62);
			instance.IOIQltyInd = view.GetString(25);
			instance.IOINaturalFlag = view.GetBool(130);
			var groupViewNoIOIQualifiers = view.GetView("NoIOIQualifiers");
			if (groupViewNoIOIQualifiers is null) return;
			
			var countNoIOIQualifiers = groupViewNoIOIQualifiers.GroupCount();
			instance.NoIOIQualifiers = new IOINoIOIQualifiers[countNoIOIQualifiers];
			for (var i = 0; i < countNoIOIQualifiers; ++i)
			{
				instance.NoIOIQualifiers[i] = new();
				instance.NoIOIQualifiers[i].Parse(groupViewNoIOIQualifiers[i]);
			}
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			instance.TransactTime = view.GetDateTime(60);
			instance.URLLink = view.GetString(149);
			var groupViewNoRoutingIDs = view.GetView("NoRoutingIDs");
			if (groupViewNoRoutingIDs is null) return;
			
			var countNoRoutingIDs = groupViewNoRoutingIDs.GroupCount();
			instance.NoRoutingIDs = new IOINoRoutingIDs[countNoRoutingIDs];
			for (var i = 0; i < countNoRoutingIDs; ++i)
			{
				instance.NoRoutingIDs[i] = new();
				instance.NoRoutingIDs[i].Parse(groupViewNoRoutingIDs[i]);
			}
			instance.SpreadToBenchmark = view.GetDouble(218);
			instance.Benchmark = view.GetString(219);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
