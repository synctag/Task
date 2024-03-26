using Microsoft.AspNetCore.Mvc;
using TestApplication.Model;
using TestApplication.Repository;
using System.Linq;


namespace TestApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IRepository<CarDetails> _cardetailsRepository;
        public SearchController(IRepository<CarDetails> CarDetailsRepositor)
        {
            _cardetailsRepository = CarDetailsRepositor;
        }
        [HttpGet("Getbysearch")]
        public async Task<IActionResult> Get([FromQuery(Name ="s")]string s)
        {
            try
            {
                var cardetails = await _cardetailsRepository.GetAllData();
                var query = (from c in cardetails select c);
                query = query.Where(c => (c.Make.Contains(s)|| c.Model.Contains(s) || c.Trim.Contains(s) || c.Engine.Contains(s)) );
                return Ok(query);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
