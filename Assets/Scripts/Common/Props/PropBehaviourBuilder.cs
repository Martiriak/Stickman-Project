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

                /* TEMPLATE
                case PropTypes.Gianni:
                    command = new GianniPropCommand();
                break;
                */

                // If no valid type or I type I don't know, a null command which does nothing,
                // just to not break things.
                default:
                    command = new NullCommand();
                break;
            }

            return command;
        }



        // Internal class: no other scripts need to know a null implementation.
        private class NullCommand : PropCommand
        {
            public override void Execute()
            {
            #if UNITY_EDITOR
                UnityEngine.Debug.LogWarning("A Prop Command doing nothing has been created! Maybe you didn't update PropBehaviourBuilder with a newly introduced PropType?");
            #endif
            }
        }
    }
}
