using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{

    public float MovementSpeed = 5.0f;
    public GameObject MovingPlatform;
    public List<Transform> WayPoints = new List<Transform>();
    public bool ShouldLoop = true;
    public float WayPlatformStopTime = 1.0f;


    private int _wayPointIndex = 0;
    private bool _shouldMove = true;
    private float _movementTime = 0.0f;
    private void Awake()
    {

    }
    private void Update()
    {
        if (Time.time >= _movementTime)
            Move();
    }

    private void Move()
    {
        _shouldMove = ShouldLoop;
        if (WayPoints.Count != 0)
        {
            if (_wayPointIndex < WayPoints.Count)
            {
                MovingPlatform.transform.position = Vector3.MoveTowards(MovingPlatform.transform.position,
              WayPoints[_wayPointIndex].transform.position, MovementSpeed * Time.deltaTime);

                if (Vector3.Distance(MovingPlatform.transform.position, WayPoints[_wayPointIndex].transform.position) <= 0.05f)
                {
                    _wayPointIndex++;
                    _movementTime = Time.time + WayPlatformStopTime;
                }
            }
            else if (_wayPointIndex >= WayPoints.Count)
            {
                if (ShouldLoop)
                    _wayPointIndex = 0;
            }
        }
    }


}
