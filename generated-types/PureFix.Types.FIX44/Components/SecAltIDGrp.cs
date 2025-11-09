using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX44.Components;

namespace PureFix.Types.FIX44.Components
{
	public sealed partial class SecAltIDGrp : IFixComponent
	{
		public sealed partial class NoSecurityAltID : IFixGroup
		{
			[TagDetails(Tag = 455, Type = TagType.String, Offset = 0, Required = false)]
			public string? SecurityAltID {get; set;}
			
			[TagDetails(Tag = 456, Type = TagType.String, Offset = 1, Required = false)]
			public string? SecurityAltIDSource {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (SecurityAltID is not null) writer.WriteString(455, SecurityAltID);
				if (SecurityAltIDSource is not null) writer.WriteString(456, SecurityAltIDSource);
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				SecurityAltID = view.GetString(455);
				SecurityAltIDSource = view.GetString(456);
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "SecurityAltID":
					{
						value = SecurityAltID;
						break;
					}
					case "SecurityAltIDSource":
					{
						value = SecurityAltIDSource;
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
				SecurityAltID = null;
				SecurityAltIDSource = null;
			}
		}
		[Group(NoOfTag = 454, Offset = 0, Required = false)]
		public NoSecurityAltID[]? SecurityAltID {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (SecurityAltID is not null && SecurityAltID.Length != 0)
			{
				writer.WriteWholeNumber(454, SecurityAltID.Length);
				for (int i = 0; i < SecurityAltID.Length; i++)
				{
					((IFixEncoder)SecurityAltID[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoSecurityAltID") is IMessageView viewNoSecurityAltID)
			{
				var count = viewNoSecurityAltID.GroupCount();
				SecurityAltID = new NoSecurityAltID[count];
				for (int i = 0; i < count; i++)
				{
					SecurityAltID[i] = new();
					((IFixParser)SecurityAltID[i]).Parse(viewNoSecurityAltID.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoSecurityAltID":
				{
					value = SecurityAltID;
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
			SecurityAltID = null;
		}
	}
}
