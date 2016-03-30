using SmartPresenter.BO.Common.Enums;

namespace SmartPresenter.BO.Common.Transitions
{
    public class RevealTransition : Transition
    {
        public override TransitionTypes Type
        {
            get { return TransitionTypes.Reveal; }
        }
    }
}
