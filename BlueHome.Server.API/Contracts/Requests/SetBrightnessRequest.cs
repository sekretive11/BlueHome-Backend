namespace BlueHome.Server.API.Contracts.Requests
{
    public record SetBrightnessRequest(int DeviceId, int Brightness);
}
