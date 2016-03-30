using SmartPresenter.BO.Common.Enums;

namespace SmartPresenter.BO.Common.Transitions
{
    public class SplitTransition : Transition
    {
        public override TransitionTypes Type
        {
            get { return TransitionTypes.Split; }
        }
    }
}
