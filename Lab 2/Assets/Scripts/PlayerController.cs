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
    private bool _faceRightState = true;
    private bool _invincible;
    private Rigidbody2D _marioBody;
    private SpriteRenderer _marioSprite;
    private Animator _marioAnimator;
    private AudioSource _marioAudio;
    private static readonly int OnGround = Animator.StringToHash("onGround");
    private static readonly int XSpeed = Animator.StringToHash("xSpeed");
    private static readonly int OnSkid = Animator.StringToHash("onSkid");

    // Start is called before the first frame update
    private void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        _marioBody = GetComponent<Rigidbody2D>();
        _marioSprite = GetComponent<SpriteRenderer>();
        _marioAnimator = GetComponent<Animator>();
        _marioAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        // toggle state
        if (Input.GetKeyDown("a") && _faceRightState)
        {
            if (Mathf.Abs(_marioBody.velocity.x) > 1.0) _marioAnimator.SetTrigger(OnSkid);
            _faceRightState = false;
            _marioSprite.flipX = true;
        }

        if (Input.GetKeyDown("d") && !_faceRightState)
        {
            if (Mathf.Abs(_marioBody.velocity.x) > 1.0) _marioAnimator.SetTrigger(OnSkid);
            _faceRightState = true;
            _marioSprite.flipX = false;
        }

        if (Input.GetKeyDown("p"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // when jumping, and Gomba is near Mario and we haven't registered our score
        if (!_onGroundState && _countScoreState)
        {
            if (Mathf.Abs(transform.position.x - enemyLocation0.position.x) < 0.5f)
            {
                _countScoreState = false;
                _score++;
            }

            if (Mathf.Abs(transform.position.x - enemyLocation1.position.x) < 0.5f)
            {
                _countScoreState = false;
                _score++;
            }
        }

        _marioAnimator.SetFloat(XSpeed, Mathf.Abs(_marioBody.velocity.x));
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
            _marioBody.angularVelocity = 0;
        }

        if (Input.GetKeyDown("space") && _onGroundState)
        {
            _marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            _onGroundState = false;
            _marioAnimator.SetBool("onGround", _onGroundState);
            _countScoreState = true; //check if Gomba is underneath
        }
    }

    // called when the cube hits the floor
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Obstacle"))
        {
            _onGroundState = true;
            _marioAnimator.SetBool(OnGround, _onGroundState);
            _countScoreState = false; // reset score state
            scoreText.text = $"Score: {_score}";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!_invincible)
            {
                Debug.Log("Collided with Gomba!");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                _score += 10;
                scoreText.text = $"Score: {_score}";
            }
        }

        if (other.gameObject.CompareTag("SimpleMushroom"))
        {
            Debug.Log("Collided with Mushroom!");
            ToggleSuperSizedMario(true);
            StartCoroutine(SuperSizedMode());
        }
    }

    private void PlayJumpSound()
    {
        _marioAudio.PlayOneShot(_marioAudio.clip);
    }

    private IEnumerator SuperSizedMode()
    {
        yield return new WaitForSeconds(5);
        ToggleSuperSizedMario(false);
    }

    private void ToggleSuperSizedMario(bool enable)
    {
        _invincible = enable;
        _marioBody.transform.localScale = enable ? new Vector3(1.5f, 1.5f, 1.5f) : Vector3.one;
        EventManager.TriggerEvent("setIsKillable", new Dictionary<string, object> { { "killable", enable } });
    }
}