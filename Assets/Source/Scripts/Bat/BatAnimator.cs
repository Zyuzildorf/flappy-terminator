using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BatAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    //public void Attack()
    //{
    //    _animator.SetTrigger(BatAnimatorData.Params.Hitting);
    //}

    //public void RestartRunAnimation()
    //{
    //    _animator.SetBool(BatAnimatorData.Params.IsRunning, true);
    //}

    //public void StopRunAnimation()
    //{
    //    _animator.SetBool(BatAnimatorData.Params.IsRunning, false);
    //}
}