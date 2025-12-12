using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX44UnitTest.Components;

namespace PureFix.Types.FIX44UnitTest.Components
{
	public sealed partial class CommissionData : IFixComponent
	{
		[TagDetails(Tag = 12, Type = TagType.Float, Offset = 0, Required = false)]
		public double? Commission {get; set;}
		
		[TagDetails(Tag = 13, Type = TagType.String, Offset = 1, Required = false)]
		public string? CommType {get; set;}
		
		[TagDetails(Tag = 479, Type = TagType.String, Offset = 2, Required = false)]
		public string? CommCurrency {get; set;}
		
		[TagDetails(Tag = 497, Type = TagType.String, Offset = 3, Required = false)]
		public string? FundRenewWaiv {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Commission is not null) writer.WriteNumber(12, Commission.Value);
			if (CommType is not null) writer.WriteString(13, CommType);
			if (CommCurrency is not null) writer.WriteString(479, CommCurrency);
			if (FundRenewWaiv is not null) writer.WriteString(497, FundRenewWaiv);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			Commission = view.GetDouble(12);
			CommType = view.GetString(13);
			CommCurrency = view.GetString(479);
			FundRenewWaiv = view.GetString(497);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "Commission":
				{
					value = Commission;
					break;
				}
				case "CommType":
				{
					value = CommType;
					break;
				}
				case "CommCurrency":
				{
					value = CommCurrency;
					break;
				}
				case "FundRenewWaiv":
				{
					value = FundRenewWaiv;
					break;
				}
				default:
				{
					return false;
				}
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			Commission = null;
			CommType = null;
			CommCurrency = null;
			FundRenewWaiv = null;
		}
	}
}
