using Acme.Logic.Interfaces;
using Acme.Models;
using Microsoft.AspNetCore.Mvc;

namespace Acme.Web.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class SerialNumberController : ControllerBase
    {
        private readonly ISerialNumberLogic _serialNumberLogic;

        public SerialNumberController(ISerialNumberLogic serialNumberLogic)
        {
            _serialNumberLogic = serialNumberLogic;
        }

        /// <summary>
        /// Creates a 100 new valid serial numbers for usage.
        /// </summary>
        [HttpPost("Create100NewSerialNumbers")]
        public ActionResult Create100NewSerialNumbers()
        {
            try
            {
                _serialNumberLogic.Create100SerialNumbersAsync().Wait();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Return a list of all the valid serial numbers.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetValidSerialNumber")]
        public ActionResult GetValidSerialNumber()
        {
            try
            {
                var result = _serialNumberLogic.GetValidSerialNumber();
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
