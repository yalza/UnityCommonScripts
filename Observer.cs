using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Observer : Singleton<Observer>
{
    private Dictionary<string, List<Action>> _listeners = new Dictionary<string, List<Action>>();

    private void RegisterObserver(string key,Action action)
    {
        List<Action> actions = new List<Action>();
        if(_listeners.ContainsKey(key))
        {
            actions = _listeners[key];
        }
        else
        {
            _listeners.Add(key, actions);
        }

        _listeners.Add(key, actions);
    }

    public void NotifyObservers(string key)
    {
        if (_listeners.ContainsKey(key))
        {
            foreach(Action a in _listeners[key])
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
            return;
        }
        Debug.LogErrorFormat("listener {0} not exist", key);
    }

    public void RemoveObserver(string key,Action value)
    {
        List<Action> actions = new List<Action>();
        if (_listeners.ContainsKey(key))
        {
            actions = _listeners[key];
        }
        else
        {
            _listeners.Add(key, actions);
        }
        _listeners[key].Remove(value);
    }
}
