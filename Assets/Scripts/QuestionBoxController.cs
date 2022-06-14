using System.Collections;
using UnityEngine;

public class QuestionBoxController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public SpringJoint2D springJoint;
    public GameObject consumablePrefab; // the spawned mushroom prefab
    public SpriteRenderer spriteRenderer;
    public Sprite usedQuestionBox; // the sprite that indicates empty box instead of a question mark
    private bool _hit;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player") || _hit) return;
        _hit = true;
        // ensure that we move this object sufficiently 
        rigidBody.AddForce(new Vector2(0, rigidBody.mass * 20), ForceMode2D.Impulse);
        // spawn the mushroom prefab slightly above the box
        var position = transform.position;
        Instantiate(consumablePrefab, new Vector3(position.x, position.y + 1.0f, position.z), Quaternion.identity);
        StartCoroutine(DisableHittable());
    }

    private bool ObjectMovedAndStopped()
    {
        return Mathf.Abs(rigidBody.velocity.magnitude) < 0.01;
    }

    private IEnumerator DisableHittable()
    {
        if (!ObjectMovedAndStopped()) yield return new WaitUntil(() => ObjectMovedAndStopped());

        // continues here when the ObjectMovedAndStopped() returns true
        spriteRenderer.sprite = usedQuestionBox; // change sprite to be "used-box" sprite
        rigidBody.bodyType = RigidbodyType2D.Static; // make the box unaffected by Physics

        // reset box position
        // transform.localPosition = Vector3.zero;
        springJoint.enabled = false; // disable spring
    }
}