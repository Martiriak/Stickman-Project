using Stickman.Managers.Speed;

namespace Stickman.Levels.Context
{
    // E' possibile fare un refactor per rendere i campi statici,
    // dato che sarebbero condivisi fra tutti i livelli...
    /// <summary>
    /// Encapsulates all the necessary classes and/or structs in order
    /// for the Level class to function. Provides indirect access to them.
    /// </summary>
    public class LevelContext
    {
        public SpeedManager SpeedManager { get; private set; }

        public LevelContext(SpeedManager speedM)
        {
            SpeedManager = speedM;
        }

        public float CurrentVelocity => SpeedManager.CurrentSpeed;
    }
}
