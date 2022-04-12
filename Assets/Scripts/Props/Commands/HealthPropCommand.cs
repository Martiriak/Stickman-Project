using Stickman.Players.Context;
using Stickman.Players;

namespace Stickman.Props.Commands
{
    public class HealthPropCommand : PropCommand
    {
        private SwordsmanLives m_lives;

        protected override void Init(PlayerContext context)
        {
            m_lives = context.LivesManager;
        }

        protected override void Exec()
        {
            m_lives.RegenLives();
        }
    }
}
