using Stickman.Managers;

namespace Stickman.Props.Commands
{
    public sealed class SlowTimePropCommand : PropCommand
    {
        public override void Execute()
        {
            GameManager.Instance.SpeedManager.SlowDown(30);
        }
    }
}
