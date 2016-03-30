using SmartPresenter.BO.Common.Enums;

namespace SmartPresenter.BO.Common.Transitions
{
    public class CoverTransition : Transition
    {
        public override TransitionTypes Type
        {
            get { return TransitionTypes.Cover; }
        }
    }
}
