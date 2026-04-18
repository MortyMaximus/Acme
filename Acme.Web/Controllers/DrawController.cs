using Acme.Logic.Interfaces;
using Acme.Models;
using Microsoft.AspNetCore.Mvc;

namespace Acme.Web.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class DrawController : ControllerBase
    {
        private readonly IDrawLogic _drawLogic;

        public DrawController(IDrawLogic drawLogic)
        {
            _drawLogic = drawLogic;
        }

        /// <summary>
        /// Get a list of all serial number with a customer attached to it. 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public ActionResult GetAll(int pageSize, int pageIndex)
        {
            var drawModels = _drawLogic.GetAllAsync(pageSize, pageIndex).Result;

            return Ok(drawModels);
        }

        /// <summary>
        /// Add or attach a customer to a serial number.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddSerialNumber")]
        public ActionResult AddSerialNumber(DrawModel model)
        {
            try
            {
                _drawLogic.AddToSerialNumber(model);

                return Ok();
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
