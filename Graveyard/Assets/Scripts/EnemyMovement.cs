using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public List<Transform> Waypoints = new List<Transform>();
    public Transform EnemyTransform;
    public float MovementSpeed = 1.0f;
   

    private int _wayPointIndex = 0;
    private Animator _animator;
    private bool _isDead = false;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        _isDead = _animator.GetBool("IsDead");

        Move();
    }

    private void Move()
    {
        if ((Waypoints.Count != 0) && !_isDead)
        {
            if ((_wayPointIndex + 1) > Waypoints.Count)
                _wayPointIndex = 0;

            EnemyTransform.position = Vector3.MoveTowards(EnemyTransform.position, Waypoints[_wayPointIndex].position, MovementSpeed * Time.deltaTime);

            if ((Vector3.Distance(EnemyTransform.position, Waypoints[_wayPointIndex].position) <= 0.05f) && ((_wayPointIndex) < Waypoints.Count))
            {
                _wayPointIndex++;
                FlipEnemy();
            }
        }
    }

    private void FlipEnemy()
    {
        Vector3 localScale = EnemyTransform.localScale;
        localScale.x *= -1;
        EnemyTransform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < Waypoints.Count - 1; i++)
        {
            Gizmos.DrawLine(Waypoints[i].position, Waypoints[i + 1].position);
        }
    }
}
