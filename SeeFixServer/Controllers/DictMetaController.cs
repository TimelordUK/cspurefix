using Microsoft.AspNetCore.Mvc;
using PureFix.LogMessageParser;

namespace SeeFixServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DictMetaController : ControllerBase
    {
        private readonly ILogger<DictMetaController> _logger;
        public IDictContainer DictContainer { get; }

        public DictMetaController(ILogger<DictMetaController> logger, IDictContainer dictContainer)
        {
            _logger = logger;
            DictContainer = dictContainer;
        }

        [HttpGet("meta")]
        public IEnumerable<DictMeta> GetMeta()
        {
            return DictContainer.Parsers.Values.Select(p => p.Meta);
        }

        [HttpGet("doc")]
        public DictDoc GetDoc()
        {
            return new DictDoc();
        }
    }
}
