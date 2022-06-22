using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ConsumableTriggerChecker : MonoBehaviour
{
    public PowerUp powerUp;
    public CustomPowerUpEvent onCollected;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        onCollected.Invoke(powerUp);
        Destroy(gameObject);
    }
}