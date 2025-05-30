using UnityEngine;

namespace Source.Scripts.Utilities
{
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

        protected void SetBoolAnimator(int variable,bool value)
        {
            _animator.SetBool(variable, value);
        }
    }
}