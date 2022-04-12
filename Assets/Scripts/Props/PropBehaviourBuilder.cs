using Stickman.Players.Context;
using Stickman.Props.Commands;

namespace Stickman.Props.Builder
{
    public static class PropBehaviourBuilder
    {
        public static PropCommand AssembleCommand(PropTypes typeToBuild)
        {
            PropCommand command;

            switch (typeToBuild)
            {
                case PropTypes.Health:
                    command = new HealthPropCommand();
                break;

                case PropTypes.Invulnerability:
                    command = new InvulnerabilityPropCommand();
                break;

                default:
                    command = new NullCommand();
                break;
            }

            return command;
        }


        // Internal class: no other scripts need to know a null implementation.
        private class NullCommand : PropCommand
        {
            protected override void Init(PlayerContext context) { }
            protected override void Exec() { }
        }
    }
}
