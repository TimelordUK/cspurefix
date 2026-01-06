using Microsoft.AspNetCore.Mvc;
using PureFix.LogMessageParser;
using System.Text.RegularExpressions;

namespace SeeFixServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogParserController : ControllerBase
    {
        private readonly IDictContainer _registry;
        private readonly ILogger<LogParserController> _logger;

        public LogParserController(IDictContainer registry, ILogger<LogParserController> logger)
        {
            _registry = registry;
            _logger = logger;
        }

        /// <summary>
        /// Get all registered dictionaries for UI dropdown
        /// </summary>
        [HttpGet("registry")]
        public ActionResult<IEnumerable<RegistryEntry>> GetRegistry()
        {
            var loadedParsers = _registry.Parsers.Keys.ToHashSet();
            var entries = _registry.GetAllMetas().Select(m => new RegistryEntry
            {
                Name = m.Name,
                DisplayName = m.DisplayName ?? m.Name,
                Version = m.Version,
                SenderCompIds = m.SenderCompIds ?? [],
                TargetCompIds = m.TargetCompIds ?? [],
                IsDefault = m.IsDefault,
                IsLoaded = loadedParsers.Contains(m.Name ?? "")
            });
            return Ok(entries);
        }

        /// <summary>
        /// Upload a FIX log file and auto-detect dictionary by tag 49 and 56
        /// </summary>
        [HttpPost("upload")]
        public async Task<ActionResult<ParseUploadResult>> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file provided");

            using var reader = new StreamReader(file.OpenReadStream());
            var content = await reader.ReadToEndAsync();
            var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(l => l.Trim())
                .Where(l => !string.IsNullOrEmpty(l))
                .ToList();

            if (lines.Count == 0)
                return BadRequest("Empty file");

            // Extract tag 49 and 56 from first message
            var (senderCompId, targetCompId) = ExtractCompIds(lines[0]);

            _logger.LogInformation("Detected SenderCompID={Sender}, TargetCompID={Target}",
                senderCompId, targetCompId);

            // Find matching parser
            var parser = _registry.FindByCompIds(senderCompId, targetCompId);

            if (parser == null)
            {
                _logger.LogWarning("No matching dictionary found for 49={Sender}, 56={Target}",
                    senderCompId, targetCompId);
                return BadRequest($"No matching dictionary found for SenderCompID={senderCompId}, TargetCompID={targetCompId}");
            }

            _logger.LogInformation("Using dictionary: {Dict}", parser.Meta.Name);

            // Detect delimiter (pipe or SOH)
            byte delim = lines[0].Contains('|') ? (byte)'|' : (byte)0x01;

            // Parse all messages
            var request = new ParseRequest
            {
                DictName = parser.Meta.Name,
                Messages = lines,
                Delim = delim
            };

            var (result, _) = parser.Parse(request);

            return Ok(new ParseUploadResult
            {
                DetectedSenderCompId = senderCompId,
                DetectedTargetCompId = targetCompId,
                UsedDictionary = parser.Meta.Name,
                UsedDictionaryDisplayName = parser.Meta.DisplayName,
                MessageCount = result.Messages.Count,
                Messages = result.Messages
            });
        }

        /// <summary>
        /// Parse messages with explicit dictionary selection
        /// </summary>
        [HttpPost("parse")]
        public ActionResult<ParseUploadResult> Parse([FromBody] ParseWithDictRequest request)
        {
            if (request.Messages == null || request.Messages.Count == 0)
                return BadRequest("No messages provided");

            var parser = _registry[request.DictName ?? ""];
            if (parser == null)
            {
                // Try auto-detect
                var (sender, target) = ExtractCompIds(request.Messages[0]);
                parser = _registry.FindByCompIds(sender, target);
            }

            if (parser == null)
                return BadRequest($"Dictionary not found: {request.DictName}");

            byte delim = GetDelimiter(request.Delimiter, request.Messages[0]);

            var parseRequest = new ParseRequest
            {
                DictName = parser.Meta.Name,
                Messages = request.Messages,
                Delim = delim
            };

            var (result, _) = parser.Parse(parseRequest);

            return Ok(new ParseUploadResult
            {
                UsedDictionary = parser.Meta.Name,
                UsedDictionaryDisplayName = parser.Meta.DisplayName,
                MessageCount = result.Messages.Count,
                Messages = result.Messages
            });
        }

        /// <summary>
        /// Get delimiter byte from request parameter or auto-detect from message
        /// </summary>
        private static byte GetDelimiter(string? delimiterParam, string message)
        {
            return delimiterParam?.ToLower() switch
            {
                "pipe" => (byte)'|',
                "soh" => 0x01,
                _ => message.Contains('|') ? (byte)'|' : (byte)0x01 // auto-detect
            };
        }

        /// <summary>
        /// Extract SenderCompID (tag 49) and TargetCompID (tag 56) from a FIX message
        /// </summary>
        private static (string? senderCompId, string? targetCompId) ExtractCompIds(string message)
        {
            string? sender = null;
            string? target = null;

            // Try pipe delimiter first
            var senderMatch = Regex.Match(message, @"\|49=([^|]+)");
            if (senderMatch.Success)
                sender = senderMatch.Groups[1].Value;

            var targetMatch = Regex.Match(message, @"\|56=([^|]+)");
            if (targetMatch.Success)
                target = targetMatch.Groups[1].Value;

            // If not found, try SOH delimiter
            if (sender == null)
            {
                senderMatch = Regex.Match(message, @"\x0149=([^\x01]+)");
                if (senderMatch.Success)
                    sender = senderMatch.Groups[1].Value;
            }

            if (target == null)
            {
                targetMatch = Regex.Match(message, @"\x0156=([^\x01]+)");
                if (targetMatch.Success)
                    target = targetMatch.Groups[1].Value;
            }

            return (sender, target);
        }
    }

    public class ParseUploadResult
    {
        public string? DetectedSenderCompId { get; set; }
        public string? DetectedTargetCompId { get; set; }
        public string? UsedDictionary { get; set; }
        public string? UsedDictionaryDisplayName { get; set; }
        public int MessageCount { get; set; }
        public List<ParsedMessage> Messages { get; set; } = [];
    }

    public class ParseWithDictRequest
    {
        public string? DictName { get; set; }
        public List<string>? Messages { get; set; }
        /// <summary>Optional delimiter: "pipe" or "soh" (default: auto-detect)</summary>
        public string? Delimiter { get; set; }
    }

    public class RegistryEntry
    {
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public string? Version { get; set; }
        public List<string> SenderCompIds { get; set; } = [];
        public List<string> TargetCompIds { get; set; } = [];
        public bool IsDefault { get; set; }
        public bool IsLoaded { get; set; }
    }
}
