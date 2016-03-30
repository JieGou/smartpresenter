using SmartPresenter.BO.Common.Enums;

namespace SmartPresenter.BO.Common.Transitions
{
    public class FadeTransition : Transition
    {
        public override TransitionTypes Type
        {
            get { return TransitionTypes.Fade; }
        }
    }
}
