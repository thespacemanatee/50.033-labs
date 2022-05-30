using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float upSpeed;
    public float maxSpeed;
    public Transform enemyLocation0;
    public Transform enemyLocation1;
    public Text scoreText;

    private int _score;
    private bool _countScoreState;
    private bool _onGroundState = true;
    private Rigidbody2D _marioBody;
    private SpriteRenderer _marioSprite;
    private bool _faceRightState = true;

    // Start is called before the first frame update
    private void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        _marioBody = GetComponent<Rigidbody2D>();
        _marioSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        // toggle state
        if (Input.GetKeyDown("a") && _faceRightState)
        {
            _faceRightState = false;
            _marioSprite.flipX = true;
        }

        if (Input.GetKeyDown("d") && !_faceRightState)
        {
            _faceRightState = true;
            _marioSprite.flipX = false;
        }

        // when jumping, and Gomba is near Mario and we haven't registered our score
        if (!_onGroundState && _countScoreState)
        {
            if (Mathf.Abs(transform.position.x - enemyLocation0.position.x) < 0.5f)
            {
                _countScoreState = false;
                _score++;
                Debug.Log(_score);
            }
            if (Mathf.Abs(transform.position.x - enemyLocation1.position.x) < 0.5f)
            {
                _countScoreState = false;
                _score++;
                Debug.Log(_score);
            }
        }
    }

    // FixedUpdate may be called once per frame. See documentation for details.
    private void FixedUpdate()
    {
        // dynamic rigidbody
        var moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0)
        {
            var movement = new Vector2(moveHorizontal, 0);
            if (_marioBody.velocity.magnitude < maxSpeed)
                _marioBody.AddForce(movement * speed);
        }

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            // stop
            _marioBody.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown("space") && _onGroundState)
        {
            _marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            _onGroundState = false;
            _countScoreState = true; //check if Gomba is underneath
        }
    }

    // called when the cube hits the floor
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            _onGroundState = true;
            _countScoreState = false; // reset score state
            scoreText.text = $"Score: {_score}";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with Gomba!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}