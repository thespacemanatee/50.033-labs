using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	public float speed = 5f;

	public Animator animator;
	public Rigidbody2D rb;

	private Vector2 _move;

	private SpriteRenderer _sr;
	private static readonly int Speed = Animator.StringToHash("Speed");

	// Start is called before the first frame update
	private void Start()
    {
		_sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
		_move.x = Input.GetAxisRaw("Horizontal");

		animator.SetFloat(Speed, Mathf.Abs(_move.x));

		if (Input.GetButtonDown("Fire1"))
		{
			animator.SetTrigger("Attack");
		}
    }

	private void FixedUpdate()
	{
		rb.MovePosition(rb.position + _move * (speed * Time.fixedDeltaTime));

		_sr.flipX = _move.x switch
		{
			< 0f => true,
			> 0f => false,
			_ => _sr.flipX
		};
	}
}
