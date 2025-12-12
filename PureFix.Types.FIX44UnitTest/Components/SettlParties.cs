using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX44UnitTest.Components;

namespace PureFix.Types.FIX44UnitTest.Components
{
	public sealed partial class SettlParties : IFixComponent
	{
		public sealed partial class NoSettlPartyIDs : IFixGroup
		{
			[TagDetails(Tag = 782, Type = TagType.String, Offset = 0, Required = false)]
			public string? SettlPartyID {get; set;}
			
			[TagDetails(Tag = 783, Type = TagType.String, Offset = 1, Required = false)]
			public string? SettlPartyIDSource {get; set;}
			
			[TagDetails(Tag = 784, Type = TagType.Int, Offset = 2, Required = false)]
			public int? SettlPartyRole {get; set;}
			
			[Component(Offset = 3, Required = false)]
			public SettlPtysSubGrp? SettlPtysSubGrp {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (SettlPartyID is not null) writer.WriteString(782, SettlPartyID);
				if (SettlPartyIDSource is not null) writer.WriteString(783, SettlPartyIDSource);
				if (SettlPartyRole is not null) writer.WriteWholeNumber(784, SettlPartyRole.Value);
				if (SettlPtysSubGrp is not null) ((IFixEncoder)SettlPtysSubGrp).Encode(writer);
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				SettlPartyID = view.GetString(782);
				SettlPartyIDSource = view.GetString(783);
				SettlPartyRole = view.GetInt32(784);
				if (view.GetView("SettlPtysSubGrp") is IMessageView viewSettlPtysSubGrp)
				{
					SettlPtysSubGrp = new();
					((IFixParser)SettlPtysSubGrp).Parse(viewSettlPtysSubGrp);
				}
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "SettlPartyID":
					{
						value = SettlPartyID;
						break;
					}
					case "SettlPartyIDSource":
					{
						value = SettlPartyIDSource;
						break;
					}
					case "SettlPartyRole":
					{
						value = SettlPartyRole;
						break;
					}
					case "SettlPtysSubGrp":
					{
						value = SettlPtysSubGrp;
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
				SettlPartyID = null;
				SettlPartyIDSource = null;
				SettlPartyRole = null;
				((IFixReset?)SettlPtysSubGrp)?.Reset();
			}
		}
		[Group(NoOfTag = 781, Offset = 0, Required = false)]
		public NoSettlPartyIDs[]? SettlPartyIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (SettlPartyIDs is not null && SettlPartyIDs.Length != 0)
			{
				writer.WriteWholeNumber(781, SettlPartyIDs.Length);
				for (int i = 0; i < SettlPartyIDs.Length; i++)
				{
					((IFixEncoder)SettlPartyIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoSettlPartyIDs") is IMessageView viewNoSettlPartyIDs)
			{
				var count = viewNoSettlPartyIDs.GroupCount();
				SettlPartyIDs = new NoSettlPartyIDs[count];
				for (int i = 0; i < count; i++)
				{
					SettlPartyIDs[i] = new();
					((IFixParser)SettlPartyIDs[i]).Parse(viewNoSettlPartyIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoSettlPartyIDs":
				{
					value = SettlPartyIDs;
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
			SettlPartyIDs = null;
		}
	}
}
