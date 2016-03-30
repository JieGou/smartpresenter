
using SmartPresenter.BO.Common.Enums;
namespace SmartPresenter.BO.Common.Transitions
{
    public class FlashTransition : Transition
    {
        public override TransitionTypes Type
        {
            get { return TransitionTypes.Flash; }
        }
    }
}
