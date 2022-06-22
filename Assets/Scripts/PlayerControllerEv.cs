using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerControllerEv : MonoBehaviour
{
    public IntVariable marioUpSpeed;
    public IntVariable marioMaxSpeed;
    public GameConstants gameConstants;
    public ParticleSystem dustCloud;
    public CustomCastEvent onCastPowerUp;

    private float _force;
    private bool _isDead;
    private bool _isADKeyDown;
    private bool _isSpacebarDown;
    private bool _faceRightState = true;
    private bool _onGroundState = true;
    private bool _countScoreState;

    private Rigidbody2D _marioBody;
    private Animator _marioAnimator;
    private SpriteRenderer _marioSprite;
    private AudioSource _marioAudio;

    private static readonly int OnGround = Animator.StringToHash("onGround");
    private static readonly int XSpeed = Animator.StringToHash("xSpeed");
    private static readonly int OnSkid = Animator.StringToHash("onSkid");
    private static readonly int IsDead = Animator.StringToHash("isDead");

    // other components and internal state
    private void Start()
    {
        _marioBody = GetComponent<Rigidbody2D>();
        _marioAnimator = GetComponent<Animator>();
        _marioSprite = GetComponent<SpriteRenderer>();
        _marioAudio = GetComponent<AudioSource>();
        marioUpSpeed.SetValue(gameConstants.playerJumpSpeed);
        marioMaxSpeed.SetValue(gameConstants.playerMaxSpeed);
        _force = gameConstants.playerDefaultForce;
    }

    private void Update()
    {
        if (_isDead) return;
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

        if (Input.GetKeyDown("z"))
        {
            Debug.Log("invoking z");
            onCastPowerUp.Invoke(KeyCode.Z);
        }

        if (Input.GetKeyDown("x"))
        {
            onCastPowerUp.Invoke(KeyCode.X);
        }

        if (Input.GetKeyDown("p")) SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        _marioAnimator.SetFloat(XSpeed, Mathf.Abs(_marioBody.velocity.x));
    }

    // FixedUpdate may be called once per frame. See documentation for details.
    private void FixedUpdate()
    {
        if (_isDead) return;
        // dynamic rigidbody
        var moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0)
        {
            var direction = _faceRightState ? 1.0f : -1.0f;
            var movement = new Vector2(_force * direction, 0);
            if (_marioBody.velocity.magnitude < marioMaxSpeed.Value)
                _marioBody.AddForce(movement);
        }

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            // stop
            _marioBody.velocity = Vector2.zero;
            _marioBody.angularVelocity = 0;
        }

        if (Input.GetKeyDown("space") && _onGroundState)
        {
            _marioBody.AddForce(Vector2.up * marioUpSpeed.Value, ForceMode2D.Impulse);
            _onGroundState = false;
            // part 2
            _marioAnimator.SetBool(OnGround, _onGroundState);
            _countScoreState = true;
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
            Debug.Log("Collided with Goomba!");
        }
    }
    
    public void PlayerDiesSequence()
    {
        Debug.Log("onPlayerDeath invoked");
        _isDead = true;
        _marioAnimator.SetBool(IsDead, true);
        GetComponent<Collider2D>().enabled = false;
        _marioBody.AddForce(Vector3.up * 30, ForceMode2D.Impulse);
        _marioBody.gravityScale = 30;
        StartCoroutine(Dead());
    }

    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(1.0f);
        _marioBody.bodyType = RigidbodyType2D.Static;
    }

    private void PlayJumpSound()
    {
        _marioAudio.PlayOneShot(_marioAudio.clip);
    }
}