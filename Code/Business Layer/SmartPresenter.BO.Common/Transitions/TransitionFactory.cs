using SmartPresenter.BO.Common.Enums;

namespace SmartPresenter.BO.Common.Transitions
{
    public static class TransitionFactory
    {
        #region Public Static Methods

        public static Transition CreateTransition(TransitionTypes type, double duration = 5)
        {
            Transition transition = new NoneTransition() { Duration = duration };

            switch (type)
            {
                case TransitionTypes.Cut:
                    transition = new CutTransition() { Duration = duration };
                    break;
                case TransitionTypes.Fade:
                    transition = new FadeTransition() { Duration = duration };
                    break;
                case TransitionTypes.Push:
                    transition = new PushTransition() { Duration = duration };
                    break;
                case TransitionTypes.Wipe:
                    transition = new WipeTransition() { Duration = duration };
                    break;
                case TransitionTypes.Split:
                    transition = new SplitTransition() { Duration = duration };
                    break;
                case TransitionTypes.Reveal:
                    transition = new RevealTransition() { Duration = duration };
                    break;
                case TransitionTypes.RandomBars:
                    transition = new RandomBarsTransition() { Duration = duration };
                    break;
                case TransitionTypes.Shape:
                    transition = new ShapeTransition() { Duration = duration };
                    break;
                case TransitionTypes.Uncover:
                    transition = new UncoverTransition() { Duration = duration };
                    break;
                case TransitionTypes.Cover:
                    transition = new CoverTransition() { Duration = duration };
                    break;
                case TransitionTypes.Flash:
                    transition = new FlashTransition() { Duration = duration };
                    break;
                case TransitionTypes.Dissolve:
                    transition = new DissolveTransition() { Duration = duration };
                    break;
            }

            return transition;
        }

        #endregion
    }
}
