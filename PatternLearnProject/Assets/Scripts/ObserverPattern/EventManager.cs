using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    Dictionary<Type, Action<IGameEvent>> listeners = new Dictionary<Type, Action<IGameEvent>>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static void Publish<T>(T gameEvent) where T : IGameEvent
    {
        if (instance.listeners.TryGetValue(typeof(T), out Action<IGameEvent> action))
        {
            action?.Invoke(gameEvent);
        }
    }
    public static void Subscribe<T>(Action<T> listener) where T : IGameEvent
    {
        Type eventType = typeof(T);
        Action<IGameEvent> wrappedListener = e => listener((T)e);

        if (instance.listeners.ContainsKey(eventType))
        {
            instance.listeners[eventType] += wrappedListener;
        }
        else
        {
            instance.listeners.Add(eventType, wrappedListener);
        }
    }
    public static void UnSubscribe<T>(Action<T> listener) where T : IGameEvent
    {
        if (instance == null) return;

        Type eventType = typeof(T);

        if (instance.listeners.TryGetValue(eventType, out Action<IGameEvent> existingAction))
        {
            Action<IGameEvent> wrappedListener = e => listener((T)e);

            existingAction -= wrappedListener;

            instance.listeners[eventType] = existingAction;

            if (existingAction == null)
            {
                instance.listeners.Remove(eventType);
            }
        }
    }
}
