using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private Vector3 _scaler;

    // Start is called before the first frame update
    private void Start()
    {
        // we want the object to have a scale of 0 (disappear) after 30 frames. 
        _scaler = transform.localScale / 30;
        _rigidBody = GetComponent<Rigidbody2D>();
        StartCoroutine(nameof(ScaleOut));
    }

    private IEnumerator ScaleOut()
    {
        var direction = new Vector2(Random.Range(-1.0f, 1.0f), 1);
        _rigidBody.AddForce(direction.normalized * 10, ForceMode2D.Impulse);
        _rigidBody.AddTorque(10, ForceMode2D.Impulse);
        // wait for next frame
        yield return null;

        // render for 0.5 second
        for (var step = 0; step < 30; step++)
        {
            transform.localScale -= _scaler;
            // wait for next frame
            yield return null;
        }

        Destroy(gameObject);
    }
}