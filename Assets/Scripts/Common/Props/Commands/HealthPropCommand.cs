using Stickman.Managers;

namespace Stickman.Props.Commands
{
    public sealed class HealthPropCommand : PropCommand
    {
        public override void Execute()
        {
            GameManager.Instance.LivesManager.AddLife();
        }
    }
}
