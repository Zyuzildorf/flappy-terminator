using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BatAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Shoot()
    {
        _animator.SetTrigger(BatAnimatorData.Params.Shooting);
    }
    public void Swing()
    {
        _animator.SetTrigger(BatAnimatorData.Params.Swinging);
    }

    public void Die()
    {
        _animator.SetBool(BatAnimatorData.Params.IsDie, true);
    }

    public void Revive()
    {
        _animator.SetBool(BatAnimatorData.Params.IsDie, false);
    }

    public void Stand()
    {
        _animator.SetBool(BatAnimatorData.Params.IsSitting, false);
    }

    public void Sit()
    {
        _animator.SetBool(BatAnimatorData.Params.IsSitting, true);
    }
}