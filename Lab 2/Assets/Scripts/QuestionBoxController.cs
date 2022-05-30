using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public SpringJoint2D springJoint;
    public GameObject consumablePrefab; // the spawned mushroom prefab
    public SpriteRenderer spriteRenderer;
    public Sprite usedQuestionBox; // the sprite that indicates empty box instead of a question mark
    private bool _hit = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && !_hit)
        {
            _hit = true;
            // spawn the mushroom prefab slightly above the box
            var position = transform.position;
            Instantiate(consumablePrefab,
                new Vector3(position.x, position.y + 1.0f, position.z),
                Quaternion.identity);
        }
    }
}