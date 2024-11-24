using Microsoft.AspNetCore.Mvc;
using PureFix.LogMessageParser;
using SeeFixServer.State;

namespace SeeFixServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        IDictContainer DictContainer { get; set; }

        public MessageController(IDictContainer dictContainer) { 
            DictContainer = dictContainer;
        }

        [HttpPost(Name = "ParseMessage")]
        public ParseResult Parse([FromBody] ParseRequest request)
        {
            if (string.IsNullOrEmpty(request.DictName)) return new ParseResult { Request = request };
            var dict = DictContainer[request.DictName];
            if (dict == null) return new ParseResult { Request = request };
            var result = dict.Parse(request);
            return result;
        }
    }
}
