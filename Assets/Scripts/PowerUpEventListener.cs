using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class CustomPowerUpEvent : UnityEvent<PowerUp>
{
}

public class PowerUpEventListener : MonoBehaviour
{
    public PowerUpEvent @event;
    public CustomPowerUpEvent response;
    private void OnEnable()
    {
        @event.RegisterListener(this);
    }

    private void OnDisable()
    {
        @event.UnregisterListener(this);
    }

    public void OnEventRaised(PowerUp p)
    {
        response.Invoke(p);
    }
}