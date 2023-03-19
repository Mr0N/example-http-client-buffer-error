using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        [HttpGet(Name = "get")]
        public FileStreamResult Get()
        {
           var stream = new RandomStream(2_500_000_000); // 2.5 gigabytes

            return File(stream, "application/vnd.microsoft.portable-executable", "random_data.exe");
        }
    }
}