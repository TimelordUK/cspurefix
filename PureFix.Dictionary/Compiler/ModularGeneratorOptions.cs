using PureFix.Dictionary.Definition;

namespace PureFix.Dictionary.Compiler
{
    /// <summary>
    /// Options for the ModularGenerator which creates separate assemblies per dictionary
    /// </summary>
    public sealed class ModularGeneratorOptions
    {
        /// <summary>
        /// Base output directory where assembly folders will be created (e.g., "generated-assemblies/")
        /// </summary>
        public required string OutputPath { get; set; }

        /// <summary>
        /// Assembly name (e.g., "PureFix.Types.FIX44Core")
        /// If not provided, will be derived from DictionaryName
        /// </summary>
        public string? AssemblyName { get; set; }

        /// <summary>
        /// Dictionary name to use for namespace generation (e.g., "FIX44Core")
        /// If not provided, will be sanitized from DictionaryPath filename
        /// </summary>
        public string? DictionaryName { get; set; }

        /// <summary>
        /// Path to the dictionary XML file (used for auto-naming if AssemblyName not provided)
        /// </summary>
        public string? DictionaryPath { get; set; }

        /// <summary>
        /// Relative path to PureFix.Types.Core.csproj from the generated assembly
        /// Default: "../../PureFix.Types.Core/PureFix.Types.Core.csproj"
        /// </summary>
        public string CoreProjectPath { get; set; } = "../../PureFix.Types.Core/PureFix.Types.Core.csproj";

        /// <summary>
        /// Base options used by GeneratorBase
        /// </summary>
        public Options BaseOptions { get; set; } = new();

        /// <summary>
        /// Creates ModularGeneratorOptions from a dictionary file path
        /// </summary>
        public static ModularGeneratorOptions FromDictionaryPath(
            string dictionaryPath,
            string outputPath,
            IFixDefinitions definitions)
        {
            var dictFileName = Path.GetFileNameWithoutExtension(dictionaryPath);
            var sanitizedName = SanitizeName(dictFileName);
            var assemblyName = $"PureFix.Types.{sanitizedName}";

            return new ModularGeneratorOptions
            {
                OutputPath = outputPath,
                AssemblyName = assemblyName,
                DictionaryName = sanitizedName,
                DictionaryPath = dictionaryPath,
                BaseOptions = new Options
                {
                    MsgTypes = definitions.Message.Select(kv => kv.Value.MsgType).Distinct().ToList(),
                    BackingTypeOutputPath = Path.Join(outputPath, assemblyName),
                    BackingTypeNamespace = assemblyName,
                }
            };
        }

        /// <summary>
        /// Sanitizes a name for use as a valid C# namespace/assembly name
        /// Removes: dashes, dots, spaces, underscores
        /// Example: "FIX44-Core.xml" -> "FIX44Core"
        /// </summary>
        public static string SanitizeName(string name)
        {
            return name
                .Replace("-", "")
                .Replace(".", "")
                .Replace(" ", "")
                .Replace("_", "");
        }
    }
}
