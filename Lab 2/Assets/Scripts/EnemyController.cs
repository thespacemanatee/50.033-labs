using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D _enemyBody;

    // Start is called before the first frame update
    private void Start()
    {
        _enemyBody = GetComponent<Rigidbody2D>();
        _enemyBody.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Pipe")) return;
        _enemyBody.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);
        moveSpeed *= -1;
    }
}