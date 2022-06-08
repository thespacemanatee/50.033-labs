using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    public bool initialLeft;
    private Rigidbody2D _enemyBody;
    private bool _isKillable;

    // Start is called before the first frame update
    private void Start()
    {
        _enemyBody = GetComponent<Rigidbody2D>();
        _enemyBody.AddForce(GetForce(Vector2.left), ForceMode2D.Impulse);
    }

    private void OnEnable()
    {
        EventManager.StartListening("setIsKillable", SetIsKillable);
    }

    private void OnDisable()
    {
        EventManager.StopListening("setIsKillable", SetIsKillable);
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Pipe"))
        {
            _enemyBody.AddForce(GetForce(Vector2.right), ForceMode2D.Impulse);
            moveSpeed *= -1;
        }

        if (col.gameObject.CompareTag("Player") && _isKillable) Destroy(gameObject);
    }

    private void SetIsKillable(Dictionary<string, object> message)
    {
        var killable = (bool)message["killable"];
        _isKillable = killable;
    }

    private Vector2 GetForce(Vector2 direction)
    {
        return direction * moveSpeed * (initialLeft ? 1 : -1);
    }
}