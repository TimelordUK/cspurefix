using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingOptionExerciseComponent : IFixComponent
	{
		[TagDetails(Tag = 41810, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingExerciseDesc {get; set;}
		
		[TagDetails(Tag = 41811, Type = TagType.Length, Offset = 1, Required = false, LinksToTag = 41812)]
		public int? EncodedUnderlyingExerciseDescLen {get; set;}
		
		[TagDetails(Tag = 41812, Type = TagType.RawData, Offset = 2, Required = false, LinksToTag = 41811)]
		public byte[]? EncodedUnderlyingExerciseDesc {get; set;}
		
		[TagDetails(Tag = 41813, Type = TagType.Boolean, Offset = 3, Required = false)]
		public bool? UnderlyingAutomaticExerciseIndicator {get; set;}
		
		[TagDetails(Tag = 41814, Type = TagType.Float, Offset = 4, Required = false)]
		public double? UnderlyingAutomaticExerciseThresholdRate {get; set;}
		
		[TagDetails(Tag = 41815, Type = TagType.Int, Offset = 5, Required = false)]
		public int? UnderlyingExerciseConfirmationMethod {get; set;}
		
		[TagDetails(Tag = 41816, Type = TagType.String, Offset = 6, Required = false)]
		public string? UnderlyingManualNoticeBusinessCenter {get; set;}
		
		[TagDetails(Tag = 41817, Type = TagType.Boolean, Offset = 7, Required = false)]
		public bool? UnderlyingFallbackExerciseIndicator {get; set;}
		
		[TagDetails(Tag = 41818, Type = TagType.Boolean, Offset = 8, Required = false)]
		public bool? UnderlyingLimitedRightToConfirmIndicator {get; set;}
		
		[TagDetails(Tag = 41819, Type = TagType.Boolean, Offset = 9, Required = false)]
		public bool? UnderlyingExerciseSplitTicketIndicator {get; set;}
		
		[TagDetails(Tag = 42887, Type = TagType.Int, Offset = 10, Required = false)]
		public int? UnderlyingSettlMethodElectingPartySide {get; set;}
		
		[Component(Offset = 11, Required = false)]
		public UnderlyingSettlMethodElectionDateComponent? UnderlyingSettlMethodElectionDate {get; set;}
		
		[Component(Offset = 12, Required = false)]
		public UnderlyingOptionExerciseDatesComponent? UnderlyingOptionExerciseDates {get; set;}
		
		[Component(Offset = 13, Required = false)]
		public UnderlyingOptionExerciseExpirationComponent? UnderlyingOptionExerciseExpiration {get; set;}
		
		[Component(Offset = 14, Required = false)]
		public UnderlyingOptionExerciseMakeWholeProvisionComponent? UnderlyingOptionExerciseMakeWholeProvision {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingExerciseDesc is not null) writer.WriteString(41810, UnderlyingExerciseDesc);
			if (EncodedUnderlyingExerciseDesc is not null)
			{
				writer.WriteWholeNumber(41811, EncodedUnderlyingExerciseDesc.Length);
				writer.WriteBuffer(41812, EncodedUnderlyingExerciseDesc);
			}
			if (UnderlyingAutomaticExerciseIndicator is not null) writer.WriteBoolean(41813, UnderlyingAutomaticExerciseIndicator.Value);
			if (UnderlyingAutomaticExerciseThresholdRate is not null) writer.WriteNumber(41814, UnderlyingAutomaticExerciseThresholdRate.Value);
			if (UnderlyingExerciseConfirmationMethod is not null) writer.WriteWholeNumber(41815, UnderlyingExerciseConfirmationMethod.Value);
			if (UnderlyingManualNoticeBusinessCenter is not null) writer.WriteString(41816, UnderlyingManualNoticeBusinessCenter);
			if (UnderlyingFallbackExerciseIndicator is not null) writer.WriteBoolean(41817, UnderlyingFallbackExerciseIndicator.Value);
			if (UnderlyingLimitedRightToConfirmIndicator is not null) writer.WriteBoolean(41818, UnderlyingLimitedRightToConfirmIndicator.Value);
			if (UnderlyingExerciseSplitTicketIndicator is not null) writer.WriteBoolean(41819, UnderlyingExerciseSplitTicketIndicator.Value);
			if (UnderlyingSettlMethodElectingPartySide is not null) writer.WriteWholeNumber(42887, UnderlyingSettlMethodElectingPartySide.Value);
			if (UnderlyingSettlMethodElectionDate is not null) ((IFixEncoder)UnderlyingSettlMethodElectionDate).Encode(writer);
			if (UnderlyingOptionExerciseDates is not null) ((IFixEncoder)UnderlyingOptionExerciseDates).Encode(writer);
			if (UnderlyingOptionExerciseExpiration is not null) ((IFixEncoder)UnderlyingOptionExerciseExpiration).Encode(writer);
			if (UnderlyingOptionExerciseMakeWholeProvision is not null) ((IFixEncoder)UnderlyingOptionExerciseMakeWholeProvision).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingExerciseDesc = view.GetString(41810);
			EncodedUnderlyingExerciseDescLen = view.GetInt32(41811);
			EncodedUnderlyingExerciseDesc = view.GetByteArray(41812);
			UnderlyingAutomaticExerciseIndicator = view.GetBool(41813);
			UnderlyingAutomaticExerciseThresholdRate = view.GetDouble(41814);
			UnderlyingExerciseConfirmationMethod = view.GetInt32(41815);
			UnderlyingManualNoticeBusinessCenter = view.GetString(41816);
			UnderlyingFallbackExerciseIndicator = view.GetBool(41817);
			UnderlyingLimitedRightToConfirmIndicator = view.GetBool(41818);
			UnderlyingExerciseSplitTicketIndicator = view.GetBool(41819);
			UnderlyingSettlMethodElectingPartySide = view.GetInt32(42887);
			if (view.GetView("UnderlyingSettlMethodElectionDate") is IMessageView viewUnderlyingSettlMethodElectionDate)
			{
				UnderlyingSettlMethodElectionDate = new();
				((IFixParser)UnderlyingSettlMethodElectionDate).Parse(viewUnderlyingSettlMethodElectionDate);
			}
			if (view.GetView("UnderlyingOptionExerciseDates") is IMessageView viewUnderlyingOptionExerciseDates)
			{
				UnderlyingOptionExerciseDates = new();
				((IFixParser)UnderlyingOptionExerciseDates).Parse(viewUnderlyingOptionExerciseDates);
			}
			if (view.GetView("UnderlyingOptionExerciseExpiration") is IMessageView viewUnderlyingOptionExerciseExpiration)
			{
				UnderlyingOptionExerciseExpiration = new();
				((IFixParser)UnderlyingOptionExerciseExpiration).Parse(viewUnderlyingOptionExerciseExpiration);
			}
			if (view.GetView("UnderlyingOptionExerciseMakeWholeProvision") is IMessageView viewUnderlyingOptionExerciseMakeWholeProvision)
			{
				UnderlyingOptionExerciseMakeWholeProvision = new();
				((IFixParser)UnderlyingOptionExerciseMakeWholeProvision).Parse(viewUnderlyingOptionExerciseMakeWholeProvision);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingExerciseDesc":
					value = UnderlyingExerciseDesc;
					break;
				case "EncodedUnderlyingExerciseDescLen":
					value = EncodedUnderlyingExerciseDescLen;
					break;
				case "EncodedUnderlyingExerciseDesc":
					value = EncodedUnderlyingExerciseDesc;
					break;
				case "UnderlyingAutomaticExerciseIndicator":
					value = UnderlyingAutomaticExerciseIndicator;
					break;
				case "UnderlyingAutomaticExerciseThresholdRate":
					value = UnderlyingAutomaticExerciseThresholdRate;
					break;
				case "UnderlyingExerciseConfirmationMethod":
					value = UnderlyingExerciseConfirmationMethod;
					break;
				case "UnderlyingManualNoticeBusinessCenter":
					value = UnderlyingManualNoticeBusinessCenter;
					break;
				case "UnderlyingFallbackExerciseIndicator":
					value = UnderlyingFallbackExerciseIndicator;
					break;
				case "UnderlyingLimitedRightToConfirmIndicator":
					value = UnderlyingLimitedRightToConfirmIndicator;
					break;
				case "UnderlyingExerciseSplitTicketIndicator":
					value = UnderlyingExerciseSplitTicketIndicator;
					break;
				case "UnderlyingSettlMethodElectingPartySide":
					value = UnderlyingSettlMethodElectingPartySide;
					break;
				case "UnderlyingSettlMethodElectionDate":
					value = UnderlyingSettlMethodElectionDate;
					break;
				case "UnderlyingOptionExerciseDates":
					value = UnderlyingOptionExerciseDates;
					break;
				case "UnderlyingOptionExerciseExpiration":
					value = UnderlyingOptionExerciseExpiration;
					break;
				case "UnderlyingOptionExerciseMakeWholeProvision":
					value = UnderlyingOptionExerciseMakeWholeProvision;
					break;
				default: return false;
			}
			return true;
		}
	}
}
