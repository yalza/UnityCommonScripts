using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;
    public static T Instance => _instance;

    private void Awake()
    {
        if(_instance == null) {
            _instance = this.GetComponent<T>();
            return;
        }
        if(_instance.gameObject.GetInstanceID() != gameObject.GetInstanceID())
        {
            Destroy(gameObject);
        }
    }
}
