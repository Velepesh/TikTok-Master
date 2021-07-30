using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimatorPlayerController
{
    public static class States
    {
        public const string Turn = nameof(Turn);
        public const string Jump = nameof(Jump);
        public const string JumpOver = nameof(JumpOver);
        public const string Dance = nameof(Dance);
        public const string IsSlidingRun = nameof(IsSlidingRun);
        public const string Fall = nameof(Fall);
        public const string Slide = nameof(Slide);
        public const string IsFastRun = nameof(IsFastRun);
    }
}
