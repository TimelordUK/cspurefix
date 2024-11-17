using Microsoft.AspNetCore.Mvc;
using PureFix.Dictionary.Contained;
using PureFix.LogMessageParser;

namespace SeeFixSerrver.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DictMetParserController : ControllerBase
    {
        private readonly ILogger<DictMetParserController> _logger;
        public IDictContainer DictContainer { get; }

        public DictMetParserController(ILogger<DictMetParserController> logger, IDictContainer dictContainer)
        {
            _logger = logger;
            DictContainer = dictContainer;
        }

        [HttpGet(Name = "GetDictMeta")]
        public IEnumerable<DictMeta> Get()
        {
            return DictContainer.Parsers.Values.Select(p => p.Meta);
        }
    }
}
