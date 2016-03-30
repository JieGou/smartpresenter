
using SmartPresenter.BO.Common.Enums;
namespace SmartPresenter.BO.Common.Transitions
{
    public class NoneTransition : Transition
    {
        public override TransitionTypes Type
        {
            get { return TransitionTypes.None; }
        }
    }
}
