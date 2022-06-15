using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0.0f, 10.0f, 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        GetComponent<Collider2D>().enabled = false;
        CentralManager.centralManagerInstance.IncreaseScore();
        CentralManager.SpawnEnemy();
        StartCoroutine(OnCollected());
    }

    private IEnumerator OnCollected()
    {
        for (var step = 0; step < 10; step++)
        {
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            // wait for next frame
            yield return null;
        }

        Destroy(gameObject);
    }
}