using SmartPresenter.BO.Common.Enums;

namespace SmartPresenter.BO.Common.Transitions
{
    public class UncoverTransition : Transition
    {
        public override TransitionTypes Type
        {
            get { return TransitionTypes.Uncover; }
        }
    }
}
