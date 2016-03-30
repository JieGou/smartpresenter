using SmartPresenter.BO.Common.Enums;

namespace SmartPresenter.BO.Common.Transitions
{
    public class CutTransition : Transition
    {
        public override TransitionTypes Type
        {
            get { return TransitionTypes.Cut; }
        }
    }
}
