using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestWebAppV3.Service;

namespace TestWebAppV3.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsAndPhotosController : ControllerBase
    {
        private readonly IDataService dataService;

        public AlbumsAndPhotosController(IDataService dataService)
        {
            this.dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        }

        [HttpGet]
        public async Task<IActionResult> GetDatasAsync()
        {
            try
            {
                var result = await dataService.GetDataAsync();
                if (result == null || result.Count() == 0)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDataAsync(int id)
        {
            try
            {
                if (id < 1)
                    return BadRequest();

                var result = await dataService.GetDataByUserIdAsync(id);
                if (result == null || result.Count() == 0)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}