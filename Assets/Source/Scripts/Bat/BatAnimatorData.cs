using UnityEngine;

public static class BatAnimatorData
{
    public static class Params
    {        
        public static readonly int Shooting = Animator.StringToHash(nameof(Shooting));
        public static readonly int Swinging = Animator.StringToHash(nameof(Swinging));
        public static readonly int IsDie = Animator.StringToHash(nameof(IsDie));
        public static readonly int IsSitting = Animator.StringToHash(nameof(IsSitting));
    }
}