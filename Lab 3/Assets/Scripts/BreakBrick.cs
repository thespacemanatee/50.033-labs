using System;
using UnityEngine;

public class BreakBrick : MonoBehaviour
{
    public GameObject prefab;
    private bool _broken;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player") || _broken) return;
        _broken = true;
        // assume we have 5 debris per box
        for (var x = 0; x < 5; x++)
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
        }

        var parent = gameObject.transform.parent;
        parent.GetComponent<SpriteRenderer>().enabled = false;
        parent.GetComponent<BoxCollider2D>().enabled = false;
        parent.GetComponent<AudioSource>().Play();
        GetComponent<EdgeCollider2D>().enabled = false;
        Destroy(gameObject);
    }
}