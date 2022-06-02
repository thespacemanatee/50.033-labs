using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, Action<Dictionary<string, object>>> _events;

    private static EventManager _eventManager;

    private static EventManager Instance
    {
        get
        {
            if (_eventManager) return _eventManager;
            _eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

            if (!_eventManager)
            {
                Debug.LogError("GameObject with EventManager was not found!");
            }
            else
            {
                _eventManager.Init();
                DontDestroyOnLoad(_eventManager);
            }

            return _eventManager;
        }
    }

    private void Init()
    {
        // Create event dictionary if it doesn't exist
        _events ??= new Dictionary<string, Action<Dictionary<string, object>>>();
    }

    /// <summary>
    /// Start listening to an event
    /// </summary>
    /// <param name="eventName">Event name</param>
    /// <param name="listener">Callback listener</param>
    public static void StartListening(string eventName, Action<Dictionary<string, object>> listener)
    {
        // Replace the listener if it already exists, otherwise add it
        if (Instance._events.TryGetValue(eventName, out var @event))
        {
            @event += listener;
            Instance._events[eventName] = @event;
        }
        else
        {
            @event += listener;
            Instance._events.Add(eventName, @event);
        }
    }

    /// <summary>
    /// Stop listening to an event
    /// </summary>
    /// <param name="eventName">Event name</param>
    /// <param name="listener">Callback listener</param>
    public static void StopListening(string eventName, Action<Dictionary<string, object>> listener)
    {
        if (_eventManager == null) return;
        if (!Instance._events.TryGetValue(eventName, out var @event)) return;
        @event -= listener;
        Instance._events[eventName] = @event;
    }

    /// <summary>
    /// Triggers the callback attached to an event with data
    /// </summary>
    /// <param name="eventName">Event name</param>
    /// <param name="message">Callback message</param>
    public static void TriggerEvent(string eventName, Dictionary<string, object> message)
    {
        // Get the event and invoke the listener callback with the message
        if (Instance._events.TryGetValue(eventName, out var @event))
        {
            @event.Invoke(message);
        }
    }
}