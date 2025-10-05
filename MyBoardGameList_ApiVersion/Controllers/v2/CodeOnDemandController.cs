using Asp.Versioning;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyBoardGameList.Controllers.v2
{
    [Route("v/{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class CodeOnDemandController : ControllerBase
    {
        [HttpGet(Name = "CodeOnDemand/Test")]
        [EnableCors("AnyOrigin_GetOnly")]
        [ResponseCache(NoStore = true)]
        public ContentResult Get()
        {
            string script = @"
                <script>
                    document.addEventListener('DOMContentLoaded', function() {
                        alert('This is a Code on Demand script from version 2.0 of the API.');
                    });
                </script>
            ";
            return Content(script, "text/html");
        }
    }
}
