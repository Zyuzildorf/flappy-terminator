using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    [RequireComponent(typeof(MoverRighToLeft), typeof(SinWaveMover))]
    public class EnemyCrucifix : Enemy, IMovable
    {
        private MoverRighToLeft _mover;
        private SinWaveMover _sinMover;

        protected override void Awake()
        {
            base.Awake();
            _mover = GetComponent<MoverRighToLeft>();
            _sinMover = GetComponent<SinWaveMover>();
        }

        private void Update()
        {
            Move();
        }

        public void Move()
        {
            _mover.MoveRightToLeft();
            _sinMover.MoveSinWave();
        }
    }
}