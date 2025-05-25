using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BatAnimator : EntityAnimator
{
    public void Shoot()
    {
        TriggerAnimator(BatAnimatorData.Params.Shooting);
    }
    public void Swing()
    {
        TriggerAnimator(BatAnimatorData.Params.Swinging);
    }

    public void Die()
    {
        SetBoolAnimator(BatAnimatorData.Params.IsDie, true);
    }

    public void Revive()
    {
        SetBoolAnimator(BatAnimatorData.Params.IsDie, false);
    }

    public void Stand()
    {
        SetBoolAnimator(BatAnimatorData.Params.IsSitting, false);
    }

    public void Sit()
    {
        SetBoolAnimator(BatAnimatorData.Params.IsSitting, true);
    }
}