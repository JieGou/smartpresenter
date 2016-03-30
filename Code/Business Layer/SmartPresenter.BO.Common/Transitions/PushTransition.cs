using SmartPresenter.BO.Common.Enums;

namespace SmartPresenter.BO.Common.Transitions
{
    public class PushTransition : Transition
    {
        public PushTransition()
        {
            TransitionOption = TransitionOptions.From_Top;

            AvailableOptions.Add(TransitionOptions.From_Top);
            AvailableOptions.Add(TransitionOptions.From_Bottom);
            AvailableOptions.Add(TransitionOptions.From_Left);
            AvailableOptions.Add(TransitionOptions.From_Right);
        }

        public override TransitionTypes Type
        {
            get { return TransitionTypes.Push; }
        }
    }
}
