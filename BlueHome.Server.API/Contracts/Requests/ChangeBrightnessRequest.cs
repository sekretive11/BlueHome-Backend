namespace BlueHome.Server.API.Contracts.Requests
{
    public record ChangeBrightnessRequest(int DeviceId, int Step);
}
