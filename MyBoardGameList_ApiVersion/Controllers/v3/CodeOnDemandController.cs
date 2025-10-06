using Asp.Versioning;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MyBoardGameList.Controllers.v3
{
    [Route("v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    [ApiVersion("3.0")]
    public class CodeOnDemandController : ControllerBase
    {
        [HttpGet(Name = "Test2")]
        [EnableCors("AnyOrigin_GetOnly")]
        [ResponseCache(NoStore = true)]
        public ContentResult Test2(int? minutesToAdd = null)
        {
            DateTime dateTime = DateTime.UtcNow;
            if (minutesToAdd.HasValue)
            {
                dateTime.AddMinutes(minutesToAdd.Value);
            }

            string script = "<script>" +
                    "window.alert('Your client supports JavaScript!" +
                    "\\r\\n\\r\\n" +
                    $"Server time (UTC): {dateTime.ToString("o")}" +
                    "\\r\\n" +
                    "Client time (UTC): ' + new Date().toISOString());" +
                "</script>" +
                "<noscript>Your client does not support JavaScript</noscript>";

            return Content(script, "text/html");
        }
    }
}
