

using AplicationCore.Interfaces;
using AplicationCore.Models;
using AplicationCore.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Verifarme.Controllers
{
    [ApiController]
    [Route("v1/store")]
    //[Authorize]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService command_;

        public StoreController(IStoreService command)
        {
            command_ = command;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(StoreCommand commandCreate)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await command_.AddStore(commandCreate);
            return result?.Succeeded ?? false
                    ? Ok(result)
                    : BadRequest(result?.message);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GeyAsync(string id)
        {       
            var result = await command_.GetAsync(id);
            return result!=null
                    ? Ok(result)
                    : BadRequest();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetNearby( double latitude, double longitude, double distanceInMeters)
        {
            var events = await command_.GetNearbyStorsAsync( latitude,longitude, distanceInMeters);

            return Ok(events);
        }

    }
}