using System.Collections;
using UnityEngine;

public class BlueMushroom : MonoBehaviour, IConsumable
{
    public Texture t;

    public void ConsumedBy(GameObject player)
    {
        // give player jump boost
        player.GetComponent<PlayerController>().maxSpeed *= 10;
        StartCoroutine(RemoveEffect(player));
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        // update UI
        CentralManager.centralManagerInstance.AddPowerUp(t, 0, this);
        GetComponent<Collider2D>().enabled = false;
    }

    private static IEnumerator RemoveEffect(GameObject player)
    {
        yield return new WaitForSeconds(5.0f);
        player.GetComponent<PlayerController>().maxSpeed /= 10;
    }
}