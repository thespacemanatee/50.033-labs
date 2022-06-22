using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomCastEvent : UnityEvent<KeyCode>
{
}

public class CastEventListener : MonoBehaviour
{
    public CastEvent @event;
    public CustomCastEvent response;
    
    private void OnEnable()
    {
        @event.RegisterListener(this);
    }

    private void OnDisable()
    {
        @event.UnregisterListener(this);
    }

    public void OnEventRaised(KeyCode k)
    {
        response.Invoke(k);
    }
}