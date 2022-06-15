using System;
using System.Collections.Generic;
using UnityEngine;

public class BreakBrick : MonoBehaviour
{
    public GameConstants gameConstants;
    public GameObject debrisPrefab;
    public GameObject coinPrefab;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        Instantiate(coinPrefab, transform.position + new Vector3(0, 5, 0), Quaternion.identity);
        // assume we have 5 debris per box
        for (var x = 0; x < gameConstants.spawnNumberOfDebris; x++)
        {
            Instantiate(debrisPrefab, transform.position, Quaternion.identity);
        }

        var parent = gameObject.transform.parent;
        parent.GetComponent<SpriteRenderer>().enabled = false;
        parent.GetComponent<BoxCollider2D>().enabled = false;
        parent.GetComponent<AudioSource>().Play();
        GetComponent<EdgeCollider2D>().enabled = false;
        Destroy(gameObject);
    }
}