using Microsoft.AspNetCore.Mvc;

namespace EduOMO.Views.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost("image")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile upload, [FromForm] bool isPost)
        {
            if (upload == null || upload.Length == 0)
                return BadRequest("No file uploaded");
            var typeFolder = string.Empty;
            if (isPost)
            {
                typeFolder = "PostImages";
            }
            else
            {
                typeFolder = "QuestionImages";
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", typeFolder);
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(upload.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await upload.CopyToAsync(stream);
            }

            var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{typeFolder}/{fileName}";

            return Ok(new { url = fileUrl });
        }
    }
}
