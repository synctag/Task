using Microsoft.AspNetCore.Mvc;
using TestApplication.Model;
using TestApplication.Repository;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;


namespace TestApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaginationController : ControllerBase
    {
        private readonly IRepository<CarDetails> _cardetailsRepository;
        public PaginationController(IRepository<CarDetails> CarDetailsRepositor)
        {
            _cardetailsRepository = CarDetailsRepositor;
        }
        [HttpGet("GetByPagination")]
        public async Task<IActionResult> Get([FromQuery]int page=1,[FromQuery]int pagesize=10)
        {
            try
            {
                var cardetails = await _cardetailsRepository.GetAllData();
                var query = (from c in cardetails select c);
                var totalcount=query.Count();
                var totalpage = (int)Math.Ceiling((decimal)totalcount / pagesize);
                var dataPerPage = query.Skip((page - 1) * pagesize).Take(pagesize).ToList();
                return Ok(dataPerPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
