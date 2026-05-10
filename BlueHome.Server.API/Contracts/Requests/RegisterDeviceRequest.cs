namespace BlueHome.Server.API.Contracts.Requests
{
    public sealed record RegisterDeviceRequest(
        int SpaceId,
        int LocationId,
        string Name,
        string Type
    );
}
