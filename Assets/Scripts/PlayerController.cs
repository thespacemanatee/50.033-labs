using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private static readonly int OnGround = Animator.StringToHash("onGround");
    private static readonly int XSpeed = Animator.StringToHash("xSpeed");
    private static readonly int OnSkid = Animator.StringToHash("onSkid");
    public float speed;
    public float upSpeed;
    public float maxSpeed;
    public Transform enemyLocation0;
    public Transform enemyLocation1;
    public ParticleSystem dustCloud;
    private bool _faceRightState = true;
    private Animator _marioAnimator;
    private AudioSource _marioAudio;
    private Rigidbody2D _marioBody;
    private SpriteRenderer _marioSprite;
    private bool _onGroundState = true;
    private bool _inputDisabled;

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

    private void OnEnable()
    {
        EventManager.StartListening("OnPlayerDeath", PlayerDiesSequence);
    }

    private void OnDisable()
    {
        EventManager.StopListening("OnPlayerDeath", PlayerDiesSequence);
    }

    // Update is called once per frame
    private void Update()
    {
        if (_inputDisabled) return;
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

        if (Input.GetKeyDown("p")) SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (Input.GetKeyDown("z"))
        {
            CentralManager.centralManagerInstance.ConsumePowerUp(KeyCode.Z, gameObject);
        }

        if (Input.GetKeyDown("x"))
        {
            CentralManager.centralManagerInstance.ConsumePowerUp(KeyCode.X, gameObject);
        }

        _marioAnimator.SetFloat(XSpeed, Mathf.Abs(_marioBody.velocity.x));
    }

    // FixedUpdate may be called once per frame. See documentation for details.
    private void FixedUpdate()
    {
        if (_inputDisabled) return;
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
            _marioAnimator.SetBool(OnGround, _onGroundState);
        }
    }

    // called when the cube hits the floor
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Obstacle"))
        {
            _onGroundState = true;
            _marioAnimator.SetBool(OnGround, _onGroundState);
            dustCloud.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with Gomba!");
        }
    }

    private void PlayJumpSound()
    {
        _marioAudio.PlayOneShot(_marioAudio.clip);
    }

    private void PlayerDiesSequence(Dictionary<string, object> message)
    {
        // Mario dies
        Debug.Log("Mario dies");
        // do whatever you want here, animate etc
        // ...
        _inputDisabled = true;
        var location = (Vector3)message["location"];
        if (location.x < _marioBody.transform.position.x)
        {
            _marioBody.AddForce(Vector2.right * 50, ForceMode2D.Impulse);
        }
        else
        {
            _marioBody.AddForce(Vector2.left * 50, ForceMode2D.Impulse);
        }

        _marioBody.AddForce(Vector2.up * 50, ForceMode2D.Impulse);
        _marioBody.freezeRotation = false;
    }
}