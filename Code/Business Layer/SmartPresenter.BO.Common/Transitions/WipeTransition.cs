using SmartPresenter.BO.Common.Enums;

namespace SmartPresenter.BO.Common.Transitions
{
    public class WipeTransition : Transition
    {
        public override TransitionTypes Type
        {
            get { return TransitionTypes.Wipe; }
        }
    }
}
