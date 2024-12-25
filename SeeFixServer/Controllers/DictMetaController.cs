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

        [HttpGet(Name = "GetDictMeta")]
        public IEnumerable<DictMeta> Get()
        {
            return DictContainer.Parsers.Values.Select(p => p.Meta);
        }

    }
}
