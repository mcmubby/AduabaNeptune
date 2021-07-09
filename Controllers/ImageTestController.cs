using System.Threading.Tasks;
using AduabaNeptune.Services;
using Microsoft.AspNetCore.Mvc;

namespace AduabaNeptune.Controllers
{
    [ApiController]
    [Route("test")]
    public class ImageTestController : ControllerBase
    {
        private readonly IImageService _im;

        public ImageTestController(IImageService im)
        {
            _im = im;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromBody]string image)
        {
            var response = await _im.UploadCustomerAvatar(image);
            return Ok(response);
        }
    }
}