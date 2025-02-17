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
            var doc = new DictDoc();
            var repo = DictContainer.Parsers.Values.FirstOrDefault(d => d.Meta.Name?.Contains("Repo") ?? false);
            if (repo == null) return doc;
            var messages = repo.Definitions.Message;
            var fields = repo.Definitions.TagToSimple;
            var keys = messages.Values.Select(m => m.MsgType).Distinct()
                .Select(mt => messages[mt]).ToList();
            doc.Messages.AddRange(keys.Select(m => new MessageMeta(m.MsgType, m.Name, m.Description)));
            doc.Fields.AddRange(fields.Values.Select(m => new FieldMeta(m.Tag, m.Name, m.Description)));
            return doc;
        }
    }
}
