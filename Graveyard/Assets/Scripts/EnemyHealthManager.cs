using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{

    public enum Health { low, medium, high };
    public Health HealthStrength = Health.low;

    private int _health;
    private int _lowHealth = 2;
    private int _mediumHealth = 4;
    private int _highHealth = 6;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        switch (HealthStrength)
        {
            case Health.low:
                _health = _lowHealth;
                break;
            case Health.medium:
                _health = _mediumHealth;
                break;
            case Health.high:
                _health = _highHealth;
                break;
            default:
                _health = _highHealth;
                break;
        }
    }

    private void CheckIfDead()
    {
        if (_health <= 0)
        {
            _animator.SetBool("IsDead", true);

        }
    }

    public void LowerHealth(int valueOfBullet)
    {
        _health -= valueOfBullet;

        CheckIfDead();
    }
}
