using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    public GameConstants gameConstants;
    private float _enemyMoveSpeed;
    private int _moveLeft;
    private SpriteRenderer _enemySprite;
    private Rigidbody2D _enemyBody;
    private AudioSource _deathSound;
    private bool _rejoicing;

    // Start is called before the first frame update
    private void Start()
    {
        _enemySprite = GetComponent<SpriteRenderer>();
        _enemyBody = GetComponent<Rigidbody2D>();
        _deathSound = GetComponent<AudioSource>();
        _moveLeft = Random.Range(0, 2) == 0 ? -1 : 1;
        _enemyMoveSpeed = gameConstants.enemyMoveSpeed;
        _enemyBody.AddForce(GetForce(Vector2.left), ForceMode2D.Impulse);
    }

    private void OnEnable()
    {
        EventManager.StartListening("OnPlayerDeath", EnemyRejoice);
    }

    private void OnDisable()
    {
        EventManager.StopListening("OnPlayerDeath", EnemyRejoice);
    }

    private void Update()
    {
        if (_rejoicing)
        {
            _enemySprite.flipX = !_enemySprite.flipX;
        }
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Pipe"))
        {
            _enemyBody.AddForce(GetForce(Vector2.right), ForceMode2D.Impulse);
            _moveLeft *= -1;
        }

        if (col.gameObject.CompareTag("Player"))
        {
            // check if collides on top
            var yOffset = (col.transform.position.y - transform.position.y);
            if (yOffset > 0.75f)
            {
                KillSelf();
            }
            else
            {
                // hurt player, implement later
                CentralManager.centralManagerInstance.DamagePlayer(_enemyBody.transform.position);
            }
        }
    }

    private void KillSelf()
    {
        // enemy dies
        if (_deathSound)
        {
            _deathSound.Play();
        }

        CentralManager.centralManagerInstance.IncreaseScore();
        CentralManager.SpawnEnemy();
        StartCoroutine(Flatten());
        Debug.Log("Kill sequence ends");
    }

    private IEnumerator Flatten()
    {
        Debug.Log("Flatten starts");
        const int steps = 5;
        const float stepper = 1.0f / steps;

        for (var i = 0; i < steps; i++)
        {
            var transform1 = transform;
            var localScale = transform1.localScale;
            localScale = new Vector3(localScale.x, localScale.y - stepper,
                localScale.z);
            transform1.localScale = localScale;

            // make sure enemy is still above ground
            transform1.position = new Vector3(transform1.position.x,
                gameConstants.groundSurface + GetComponent<SpriteRenderer>().bounds.extents.y, transform.position.z);
            yield return null;
        }

        Debug.Log("Flatten ends");
        gameObject.SetActive(false);
        Debug.Log("Enemy returned to pool");
    }

    // animation when player is dead
    private void EnemyRejoice(Dictionary<string, object> message)
    {
        Debug.Log("Enemy killed Mario");
        // do whatever you want here, animate etc
        // ...
        _rejoicing = true;
    }

    private Vector2 GetForce(Vector2 direction)
    {
        return direction * _enemyMoveSpeed * _moveLeft;
    }
}