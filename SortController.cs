using Microsoft.AspNetCore.Mvc;
using TestApplication.Model;
using TestApplication.Repository;
using System.Linq;


namespace TestApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SortController : ControllerBase
    {
        private readonly IRepository<CarDetails> _cardetailsRepository;
        public SortController(IRepository<CarDetails> CarDetailsRepositor)
        {
            _cardetailsRepository = CarDetailsRepositor;
        }
        [HttpGet("Getbysearch")]
        public async Task<IActionResult> Get([FromQuery(Name = "sort")] string sort)
        {
            try
            {
                var cardetails = await _cardetailsRepository.GetAllData();
                var query = (from c in cardetails select c);
                if (sort == "asc") {
                    query = query.OrderBy(c => c.Year);
                        }
                if (sort == "desc")
                {
                    query = query.OrderByDescending(c => c.Year);
                }
                return Ok(query);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
