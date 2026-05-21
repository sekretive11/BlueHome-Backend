namespace BlueHome.Server.API.Contracts.Requests
{
    public class CreateLocationRequest
    {
        public string Name { get; set; } = null!;
        public int SpaceId { get; set; }
    }
}
