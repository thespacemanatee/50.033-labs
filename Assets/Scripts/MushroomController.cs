using System.Collections;
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

    private void OnBecameInvisible()
    {
        GetComponent<SpriteRenderer>().enabled = false;
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
            StartCoroutine(CollectedAnimation());
        }
    }

    private IEnumerator CollectedAnimation()
    {
        Debug.Log("Collected mushroom");
        const int steps = 5;
        const float stepper = 1.0f / steps;

        for (var i = 0; i < steps; i++)
        {
            var transform1 = transform;
            var localScale = transform1.localScale;
            localScale = new Vector3(localScale.x + stepper, localScale.y + stepper,
                localScale.z + stepper);
            transform1.localScale = localScale;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}