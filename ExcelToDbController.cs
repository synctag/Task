using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using TestApplication.Data;
using TestApplication.Model;
using TestApplication.Repository;

namespace TestApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelToDbController : ControllerBase
    {
        private readonly IRepository<CarDetails> _cardetailsRepository;
        public ExcelToDbController(IRepository<CarDetails> CarDetailsRepositor)
        {
            _cardetailsRepository = CarDetailsRepositor;
        }
        [HttpPost("UploadExcel")]
        public async Task<IActionResult> Post([FromForm] IFormFile file)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            if (file == null || file.Length == 0)
            {
                return BadRequest("File not uploaded");
            }
            var uploadFile = $"{Directory.GetCurrentDirectory()}";
            if (!Directory.Exists(uploadFile))
            {
                Directory.CreateDirectory(uploadFile);
            }
            var filePath = Path.Combine(uploadFile, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    bool isheaderSkipped = false;
                    do
                    {
                        while (reader.Read())
                        {
                            if ((!isheaderSkipped))
                            {
                                isheaderSkipped = true;
                                continue;
                            }
                            CarDetails c = new CarDetails();
                            c.Make = reader.GetValue(0).ToString();
                            c.Model = reader.GetValue(1).ToString();
                            c.Year = Convert.ToInt32(reader.GetValue(2));
                            c.Trim =reader.GetValue(3).ToString();
                            c.Engine = reader.GetValue(4).ToString();
                            c.Status = Convert.ToBoolean(reader.GetValue(5));
                            var createCarDetailsResponse = await _cardetailsRepository.AddData(c);                           
                        }
                    } while (reader.NextResult());

                }

            }
            return Ok("Saved to DB successfully");
        }
    }
}
