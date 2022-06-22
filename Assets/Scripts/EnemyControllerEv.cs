using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyControllerEv : MonoBehaviour
{
    public AudioSource enemyAudioSource;
    public GameConstants gameConstants;
    public UnityEvent onPlayerDeath;
    public UnityEvent onEnemyDeath;
    
    private float _enemyMoveSpeed;
    private int _moveLeft;
    private SpriteRenderer _enemySprite;
    private Rigidbody2D _enemyBody;
    private AudioSource _deathSound;
    private bool _rejoicing;
    
    private static readonly int PlayerIsDead = Animator.StringToHash("playerIsDead");

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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // check if it collides with Mario
        if (other.gameObject.CompareTag("Player"))
        {
            // check if collides on top
            var yOffset = (other.transform.position.y - transform.position.y);
            if (yOffset > 0.75f)
            {
                enemyAudioSource.PlayOneShot(enemyAudioSource.clip);
                KillSelf();
                onEnemyDeath.Invoke();
            }
            else
            {
                // hurt player
                Debug.Log("invoking onPlayerDeath");
                onPlayerDeath.Invoke();
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

    public void PlayerDeathResponse()
    {
        // GetComponent<Animator>().SetBool(PlayerIsDead, true);
        _enemyBody.velocity = Vector2.zero;
        _rejoicing = true;
    }

    private Vector2 GetForce(Vector2 direction)
    {
        return direction * _enemyMoveSpeed * _moveLeft;
    }
}
