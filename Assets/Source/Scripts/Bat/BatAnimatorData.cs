using UnityEngine;

public static class BatAnimatorData
{
    public static class Params
    {
        public static readonly int Sitting = Animator.StringToHash(nameof(Sitting));
        public static readonly int Falling = Animator.StringToHash(nameof(Falling));
    }
}