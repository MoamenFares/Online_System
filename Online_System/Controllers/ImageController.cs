using Online_System.Data;
using Online_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Online_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly Online_Store_Context _context;
        public ImageController(Online_Store_Context context)
        {
            _context = context;

        }
        [HttpPost("{id}")]
        public async Task<IActionResult> Post(int id, [FromForm] FileUpload fileUpload)
        {
            try
            {
                if (fileUpload.files.Length > 0)
                {
                    Stream stream = fileUpload.files.OpenReadStream();
                       BinaryReader binaryReader = new BinaryReader(stream);
                    Byte[] bytes = binaryReader.ReadBytes((int)stream.Length);
                       _context.Products.FirstOrDefault(a => a.Id == id).Image = bytes;
                      _context.SaveChanges();
                    //var image = await SetImage(Id, fileUpload.files);
                    string path = "https://localhost:7152/api/Image?id=" + id.ToString();
                    return Ok(new { image = path });
                }
                else
                {
                    return BadRequest("Failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]

        public async Task<IActionResult> Get(int id)
        {
            var product=await _context.Products.FindAsync(id);
             
            var image = product?.Image;
            if (image == null)
            {
                return NotFound();
            }
            return File(image, "image/jpg");
        }
    }
    public class FileUpload
    {
        public IFormFile files { get; set; }
    }
}
