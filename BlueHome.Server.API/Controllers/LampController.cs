using BlueHome.Server.API.Contracts.Requests;
using BlueHome.Server.Application.CommandHandlers;
using BlueHome.Server.Application.Commands;
using Microsoft.AspNetCore.Mvc;

namespace BlueHome.Server.API.Controllers
{
    [ApiController]
    [Route("api/lamp")]
    public class LampController : ControllerBase
    {
        private readonly TurnLampOnHandler _turnOnHandler;
        private readonly TurnLampOffHandler _turnOffHandler;
        private readonly SetLampBrightnessHandler _brightnessHandler;

        public LampController(
            TurnLampOnHandler turnOnHandler,
            TurnLampOffHandler turnOffHandler,
            SetLampBrightnessHandler brightnessHandler)
        {
            _turnOnHandler = turnOnHandler;
            _turnOffHandler = turnOffHandler;
            _brightnessHandler = brightnessHandler;
        }

        // 🔌 Включить лампу
        [HttpPost("on")]
        public IActionResult TurnOn([FromBody] TurnLampOnRequest request)
        {
            var command = new TurnLampOnCommand(request.DeviceId);
            _turnOnHandler.Handle(command);

            return Ok(new { message = "Lamp turned ON" });
        }

        // 🔌 Выключить лампу
        [HttpPost("off")]
        public IActionResult TurnOff([FromBody] TurnLampOffRequest request)
        {
            var command = new TurnLampOffCommand(request.DeviceId);
            _turnOffHandler.Handle(command);

            return Ok(new { message = "Lamp turned OFF" });
        }

        // 💡 Яркость
        [HttpPost("brightness")]
        public IActionResult SetBrightness([FromBody] SetBrightnessRequest request)
        {
            if (request.Brightness < 0 || request.Brightness > 100)
                return BadRequest("Brightness must be 0-100");

            var command = new SetLampBrightnessCommand(request.DeviceId, request.Brightness);
            _brightnessHandler.Handle(command);

            return Ok(new { message = "Brightness updated" });
        }
    }
}
