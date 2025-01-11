using Microsoft.AspNetCore.Mvc;
using PureFix.LogMessageParser;

namespace SeeFixServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private IDictContainer DictContainer { get; }

        public MessageController(IDictContainer dictContainer) { 
            DictContainer = dictContainer;
        }

        [HttpPost(Name = "Parse")]
        public ParseResult Parse([FromBody] ParseRequest request)
        {
            var messages = request.Messages;
            if (messages == null || messages.Count == 0)
            {
                return new ParseResult { Request = request };
            }

            if (messages[0].EndsWith(".log"))
            {
                messages = System.IO.File.ReadAllLines(messages[0]).ToList() ?? [];
                request.Messages = messages;
            }
           
            if (string.IsNullOrEmpty(request.DictName)) return new ParseResult { Request = request };
            var dict = DictContainer[request.DictName];
            if (dict == null) return new ParseResult { Request = request };
            var result = dict.Parse(request);
            return result;
        }
    }
}
