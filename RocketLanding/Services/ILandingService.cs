namespace RocketLanding.Services
{
    public interface ILandingService
    {
        public string CheckPlatform(int rocketId, int positionX, int positionY);
    }
}