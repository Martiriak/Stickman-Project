using Stickman.Managers.Speed;

namespace Stickman.Levels.Context
{
    // E' possibile fare un refactor per rendere i campi statici,
    // dato che sarebbero condivisi fra tutti i livelli...
    public class LevelContext
    {
        public SpeedManager SpeedManager { get; private set; }

        public LevelContext(SpeedManager speedM)
        {
            SpeedManager = speedM;
        }

        public float CurrentVelocity
        {
            get => SpeedManager.EvaluateSpeed();
        }
    }
}
