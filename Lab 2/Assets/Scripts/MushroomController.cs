using UnityEngine;

public class MushroomController : MonoBehaviour
{
    public float upSpeed;
    public float moveSpeed;
    private Rigidbody2D _mushroomBody;

    // Start is called before the first frame update
    private void Start()
    {
        _mushroomBody = GetComponent<Rigidbody2D>();
        _mushroomBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
        _mushroomBody.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Pipe"))
        {
            _mushroomBody.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);
            moveSpeed *= -1;
        }

        if (col.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}