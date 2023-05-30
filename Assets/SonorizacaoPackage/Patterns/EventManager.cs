using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Patterns
{
  public abstract class EventManager<T> : Singleton<EventManager<T>>
  {
    private Dictionary<string, UnityEvent<T>> eventDictionary = new Dictionary<string, UnityEvent<T>>();

    public static void Subscribe(string eventName, UnityAction<T> listener)
    {
      UnityEvent<T> thisEvent = null;
      if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
      {
        thisEvent.AddListener(listener);
      }
      else
      {
        thisEvent = new UnityEvent<T>();
        thisEvent.AddListener(listener);
        Instance.eventDictionary.Add(eventName, thisEvent);
      }
    }

    public static void Unsubscribe(string eventName, UnityAction<T> listener)
    {
      if (Instance == null) return;
      UnityEvent<T> thisEvent = null;
      if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
      {
        thisEvent.RemoveListener(listener);
      }
    }

    public static void TriggerEvent(string eventName, T data)
    {
      UnityEvent<T> thisEvent = null;
      if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
      {
        thisEvent.Invoke(data);
      }
    }
  }
}

