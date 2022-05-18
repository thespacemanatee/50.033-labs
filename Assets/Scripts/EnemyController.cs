using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private const float MaxOffset = 5.0f;
    private const float EnemyPatrolTime = 2.0f;

    private float _originalX;
    private int _moveRight = -1;
    private Vector2 _velocity;

    private Rigidbody2D _enemyBody;

    // Start is called before the first frame update
    private void Start()
    {
        _enemyBody = GetComponent<Rigidbody2D>();
        // get the starting position
        _originalX = transform.position.x;
        ComputeVelocity();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Mathf.Abs(_enemyBody.position.x - _originalX) < MaxOffset)
        {
            // move gomba
            MoveGomba();
        }
        else
        {
            // change direction
            _moveRight *= -1;
            ComputeVelocity();
            MoveGomba();
        }
    }

    private void ComputeVelocity()
    {
        _velocity = new Vector2((_moveRight) * MaxOffset / EnemyPatrolTime, 0);
    }

    void MoveGomba()
    {
        _enemyBody.MovePosition(_enemyBody.position + _velocity * Time.fixedDeltaTime);
    }
}