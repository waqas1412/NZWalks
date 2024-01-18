using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto requestDto)
        {
            ValidateFileUpload(requestDto);

            if(ModelState.IsValid)
            {
                // Repository to upload Image
            }

            return BadRequest(ModelState);

        }

        private void ValidateFileUpload(ImageUploadRequestDto requestDto)
        {
            var allowedExtension = new string[] { "jpg", "jpeg", "png" };

            if(!allowedExtension.Contains(Path.GetExtension(requestDto.File.FileName))) {
                ModelState.AddModelError("File", "Unsupported Format");
            }

            if(requestDto.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "File size exceeded 10MB");
            }
        }
    }
}
