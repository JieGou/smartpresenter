using SmartPresenter.BO.Common.Enums;

namespace SmartPresenter.BO.Common.Transitions
{
    public class DissolveTransition : Transition
    {
        public override TransitionTypes Type
        {
            get { return TransitionTypes.Dissolve; }
        }
    }
}
