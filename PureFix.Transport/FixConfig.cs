using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Buffer.Ascii;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types;
using PureFix.Types.Config;

namespace PureFix.Transport
{
    public class FixConfig : IFixConfig
    {
        public byte LogDelimiter
        {
            get => Description?.Application?.LogDelimiter ?? AsciiChars.Soh;
            set
            {
                if (Description?.Application != null)
                {
                    Description.Application.LogDelimiter = value;
                }
            }
        }

        public byte Delimiter
        {
            get => Description?.Application?.Delimiter ?? AsciiChars.Soh;
            set
            {
                if (Description?.Application != null)
                {
                    Description.Application.Delimiter = value;
                }
            }
        }

        public byte StoreDelimiter
        {
            get => Description?.Application?.StoreDelimiter ?? AsciiChars.Soh;
            set
            {
                if (Description?.Application != null)
                {
                    Description.Application.StoreDelimiter = value;
                }
            }
        }

        public IFixDefinitions? Definitions { get; set; }
        public ISessionDescription? Description { get; set; }
        public ISessionMessageFactory? MessageFactory { get; set; }

        /// <summary>
        /// Factory for creating session stores. Defaults to MemorySessionStoreFactory if not set.
        /// Use FileSessionStoreFactory for disk persistence (QuickFix-compatible format).
        /// </summary>
        public IFixSessionStoreFactory? SessionStoreFactory { get; set; }

        public static IFixConfig MakeConfigFromPaths(string dictionaryRootPath, string sessionDescriptionPath)
        {
            var definitions = new FixDefinitions();
            var qfParser = new QuickFixXmlFileParser(definitions);
            using var streamReader = File.OpenText(sessionDescriptionPath);
            var all = streamReader.ReadToEnd();
            var sessionDescription = JsonHelper.FromJson<SessionDescription>(all);
            var definitionsPath = Path.Join(dictionaryRootPath, sessionDescription?.Application?.Dictionary ?? "FIX.xml");
            qfParser.Parse(definitionsPath);
            if (sessionDescription != null)
            {
                var config = new FixConfig
                {
                    Definitions = definitions,
                    Description = sessionDescription,
                    // Note: MessageFactory should be set by the application using the appropriate
                    // generated types (e.g., Fix44ModularSessionMessageFactory from test helpers)
                    MessageFactory = null
                };

                // Auto-create session store factory from config
                config.SessionStoreFactory = CreateStoreFactory(sessionDescription.Store);

                return config;
            }
            return new FixConfig();
        }

        /// <summary>
        /// Creates a session store factory from StoreConfig.
        /// </summary>
        private static IFixSessionStoreFactory CreateStoreFactory(StoreConfig? storeConfig)
        {
            if (storeConfig == null)
            {
                return new MemorySessionStoreFactory();
            }

            return storeConfig.Type?.ToLowerInvariant() switch
            {
                "file" when !string.IsNullOrEmpty(storeConfig.Directory)
                    => new FileSessionStoreFactory(storeConfig.Directory),
                "file" => new FileSessionStoreFactory("store"),
                _ => new MemorySessionStoreFactory()
            };
        }
    }
}
