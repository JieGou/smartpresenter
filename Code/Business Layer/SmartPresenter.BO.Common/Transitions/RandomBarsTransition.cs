using SmartPresenter.BO.Common.Enums;

namespace SmartPresenter.BO.Common.Transitions
{
    public class RandomBarsTransition : Transition
    {
        public override TransitionTypes Type
        {
            get { return TransitionTypes.RandomBars; }
        }
    }
}
