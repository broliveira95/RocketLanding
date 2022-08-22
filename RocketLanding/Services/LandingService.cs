using Microsoft.Extensions.Options;
using RocketLanding.Models;

namespace RocketLanding.Services
{
    public class LandingService : ILandingService
    {
        private static Dictionary<int, (int, int)> _data;
        private readonly IOptions<LandingOptions> _landingOptions;

        public LandingService(IOptions<LandingOptions> landingOptions)
        {
            _landingOptions = landingOptions;

            if (_data is null)
            {
                _data = new Dictionary<int, (int, int)>();
            }

        }

        public string CheckPlatform(int rocketId, int positionX, int positionY)
        {
            var platformSize = _landingOptions.Value.PlatformSize;
            var landingAreaSize = _landingOptions.Value.LandingAreaSize;

            // base validations
            if (rocketId < 0 ||
                positionX < 0 ||
                positionY < 0 ||
                positionX > landingAreaSize ||
                positionY > landingAreaSize)
            {
                return "out of landing area";
            }

            // first rocket
            if (_data.Count == 0)
            {
                _data[rocketId] = (positionX, positionY);
                return "ok for landing";
            }

            // checks if rocket is already registred
            if (_data.ContainsKey(rocketId))
            {
                var trajectory = _data[rocketId];

                if (positionX >= trajectory.Item1 &&
                    positionX <= trajectory.Item1 + platformSize &&
                    positionY >= trajectory.Item2 &&
                    positionY <= trajectory.Item2 + platformSize)
                {
                    // updates with new values
                    _data[rocketId] = (positionX, positionY);
                    return "ok for landing";
                }
                else
                {
                    return "out of platform";
                }
            }

            // checks if position for landing is available
            foreach (var item in _data)
            {
                if (positionX >= item.Value.Item1 - 1 &&
                    positionX <= item.Value.Item1 + 1 &&
                    positionY >= item.Value.Item2 - 1 &&
                    positionY <= item.Value.Item2 + 1)
                {
                    return "clash";
                }
            }

            // passed all validations
            // we can add the record
            _data[rocketId] = (positionX, positionY);

            return "ok for landing";
        }

    }
}
