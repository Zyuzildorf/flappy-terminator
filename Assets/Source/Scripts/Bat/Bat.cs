using System;
using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(BatMover), typeof(BatAttacker))]
[RequireComponent (typeof(BatScoreCounter),typeof(BatCollisionHandler))]
public class Bat : MonoBehaviour
{
    private InputReader _inputReader;
    private BatMover _mover;
    private BatAttacker _attacker;
    private BatScoreCounter _scoreCounter;
    private BatCollisionHandler _collisionHandler;

    public event Action GameOver;

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<BatMover>();
        _attacker = GetComponent<BatAttacker>();
        _scoreCounter = GetComponent<BatScoreCounter>();
        _collisionHandler = GetComponent<BatCollisionHandler>();
    }

    private void OnEnable()
    {
        _collisionHandler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _collisionHandler.CollisionDetected -= ProcessCollision;
    }

    private void Update()
    {
        if(_inputReader.IsSpacebarPressed)
        {
            _mover.Move();
        }

        if(_inputReader.IsFKeyPressed)
        {
            _attacker.Attack();
        }

        _mover.Fall();
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Obstacle)
        {
            GameOver?.Invoke();
        }
    }

    public void Reset()
    {
        _mover.ResetPosition();
        _scoreCounter.ResetScore();
    }
}
