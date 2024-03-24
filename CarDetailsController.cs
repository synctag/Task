using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Security.Cryptography.Xml;
using TestApplication.Model;
using TestApplication.Repository;

namespace TestApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarDetailsController : ControllerBase
    {
        private readonly IRepository<CarDetails> _cardetailsRepository;
        public CarDetailsController(IRepository<CarDetails> CarDetailsRepositor)
        {
            _cardetailsRepository = CarDetailsRepositor;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var cardetails = await _cardetailsRepository.GetAllData();
                return Ok(cardetails);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var cardetail = await _cardetailsRepository.GetByIdData(id);
                if (cardetail == null)
                {
                    return NotFound();
                }
                return Ok(cardetail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CarDetailReq carDetails)
        {
            try
            {
                var cardetailsEntity = new CarDetails()
                {
                    Make = carDetails.Make,
                    Model = carDetails.Model,
                    Year = carDetails.Year,
                    Trim = carDetails.Trim,
                    Engine = carDetails.Engine,
                    Status = carDetails.Status
                };
                var createCarDetailsResponse = await _cardetailsRepository.AddData(cardetailsEntity);
                return CreatedAtAction(nameof(GetById), new { id = createCarDetailsResponse.Id }, createCarDetailsResponse);
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message); 
            }

        }
        [HttpPut("{id}")]

        public async Task<IActionResult> Put(int id, [FromBody] CarDetailReq cardetail)
        {
            try
            {
                var cardetailEntity = await _cardetailsRepository.GetByIdData(id);
                if (cardetailEntity == null)
                {
                    return NotFound();
                }
                cardetailEntity.Make = cardetail.Make;
                cardetailEntity.Model = cardetail.Model;
                cardetailEntity.Year = cardetail.Year;
                cardetailEntity.Trim = cardetail.Trim;
                cardetailEntity.Engine = cardetail.Engine;
                cardetailEntity.Status = cardetail.Status;
                await _cardetailsRepository.UpdateData(cardetailEntity);
                return Ok("updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var cardetail = await _cardetailsRepository.GetByIdData(id);
                if (cardetail == null)
                {
                    return NotFound();
                }
                await _cardetailsRepository.DeleteData(cardetail);
                return Ok("Deleted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
