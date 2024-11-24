using Microsoft.AspNetCore.Mvc;
using PureFix.Dictionary.Contained;
using PureFix.LogMessageParser;
using SeeFixServer.State;

namespace SeeFixSerrver.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DictMetaParserController : ControllerBase
    {
        private readonly ILogger<DictMetaParserController> _logger;
        public IDictContainer DictContainer { get; }

        public DictMetaParserController(ILogger<DictMetaParserController> logger, IDictContainer dictContainer)
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
