using Stickman.Players.Context;

namespace Stickman.Props
{
    public abstract class PropCommand
    {
        public void Execute(PlayerContext context)
        {
            Init(context);
            Exec();
        }

        protected abstract void Init(PlayerContext context);
        protected abstract void Exec();
    }
}
