using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CastEvent", menuName = "ScriptableObjects/CastEvent", order = 3)]
public class CastEvent : ScriptableObject
{
    private readonly List<CastEventListener> _eventListeners = new();

    public void Raise(KeyCode k)
    {
        for (var i = _eventListeners.Count - 1; i >= 0; i--)
            _eventListeners[i].OnEventRaised(k);
    }

    public void RegisterListener(CastEventListener listener)
    {
        if (!_eventListeners.Contains(listener))
            _eventListeners.Add(listener);
    }

    public void UnregisterListener(CastEventListener listener)
    {
        if (_eventListeners.Contains(listener))
            _eventListeners.Remove(listener);
    }
}