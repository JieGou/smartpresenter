﻿using SmartPresenter.BO.Common.Enums;

namespace SmartPresenter.BO.Common.Transitions
{
    public class ShapeTransition : Transition
    {
        public override TransitionTypes Type
        {
            get { return TransitionTypes.Shape; }
        }
    }
}
