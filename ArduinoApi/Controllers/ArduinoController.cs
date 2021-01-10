using ArduinoTest;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArduinoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArduinoController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> SwitchLED([FromQuery] int state)
        {
            try
            {
                Arduino arduino = new Arduino();
                arduino.SwitchLED(state);
                arduino.Disconnect();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
