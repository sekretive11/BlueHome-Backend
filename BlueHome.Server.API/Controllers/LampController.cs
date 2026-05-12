using BlueHome.Server.API.Contracts.Requests;
using BlueHome.Server.Application.CommandHandlers;
using BlueHome.Server.Application.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlueHome.Server.API.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> TurnOn([FromBody] TurnLampOnRequest request)
        {
            var command = new TurnLampOnCommand(request.DeviceId);
            await _turnOnHandler.Handle(command);

            return Ok(new { message = "Lamp turned ON" });
        }

        // 🔌 Выключить лампу
        [HttpPost("off")]
        public async Task<IActionResult> TurnOff([FromBody] TurnLampOffRequest request)
        {
            var command = new TurnLampOffCommand(request.DeviceId);
            await _turnOffHandler.Handle(command);

            return Ok(new { message = "Lamp turned OFF" });
        }

        [HttpPost("brightness")]
        public async Task<IActionResult> SetBrightness(
            [FromBody] SetBrightnessRequest request)
        {
            await _brightnessHandler.Handle(
                new SetLampBrightnessCommand(
                    request.DeviceId,
                    request.Brightness
                )
            );

            return Ok();
        }
    }
}
