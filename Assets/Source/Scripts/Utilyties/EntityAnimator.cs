using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EntityAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    protected void TriggerAnimator(int trigger)
    {
        _animator.SetTrigger(trigger);
    }

    protected void SetBoolAnimator(int varible,bool value)
    {
        _animator.SetBool(varible, value);
    }
}