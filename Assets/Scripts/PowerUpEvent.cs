using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PowerUpEvent", menuName = "ScriptableObjects/PowerUpEvent", order = 3)]
public class PowerUpEvent : ScriptableObject
{
    private readonly List<PowerUpEventListener> _eventListeners = new();

    public void Raise(PowerUp p)
    {
        for (var i = _eventListeners.Count - 1; i >= 0; i--)
            _eventListeners[i].OnEventRaised(p);
    }

    public void RegisterListener(PowerUpEventListener listener)
    {
        if (!_eventListeners.Contains(listener))
            _eventListeners.Add(listener);
    }

    public void UnregisterListener(PowerUpEventListener listener)
    {
        if (_eventListeners.Contains(listener))
            _eventListeners.Remove(listener);
    }
}