using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Observer : Singleton<Observer>
{
    private Dictionary<string, List<Action>> _listeners = new Dictionary<string, List<Action>>();

    public void RegisterObserver(string key, Action action)
    {
        List<Action> actions;
        if (_listeners.ContainsKey(key))
        {
            actions = _listeners[key];
        }
        else
        {
            actions = new List<Action>();
            _listeners.Add(key, actions);
        }

        actions.Add(action);
    }

    public void NotifyObservers(string key)
    {
        if (_listeners.ContainsKey(key))
        {
            foreach (Action a in _listeners[key])
            {
                try
                {
                    a?.Invoke();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }
        else
        {
            Debug.LogErrorFormat("listener {0} not exist", key);
        }
    }

    public void RemoveObserver(string key, Action action)
    {
        if (_listeners.ContainsKey(key))
        {
            _listeners[key].Remove(action);
        }
    }
}
