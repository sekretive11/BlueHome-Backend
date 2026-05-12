namespace BlueHome.Server.API.Contracts.Requests
{
    public sealed record MoveToLocationRequest(int DeviceId, int LocationId);
}
