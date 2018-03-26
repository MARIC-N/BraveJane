using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControlle : MonoBehaviour
{
    public Vector2 Speed;
    public int ValueOfDestruction = 0;

    private Rigidbody2D _rb;

    private int _enemyLayer;

    private void Awake()
    {
        _enemyLayer = LayerMask.NameToLayer("EnemyLayer");
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = Speed;
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = Speed;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.gameObject);

        if (other.gameObject.layer == _enemyLayer)
        {
            EnemyHealthManager enemyHealth = other.gameObject.GetComponent<EnemyHealthManager>();
            enemyHealth.LowerHealth(ValueOfDestruction);
        }
    }
}
