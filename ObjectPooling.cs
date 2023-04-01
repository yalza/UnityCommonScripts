using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
    Dictionary<GameObject, List<GameObject>> _listObject = new Dictionary<GameObject, List<GameObject>>();

    public GameObject GetGameObject(GameObject obj)
    {
        if (_listObject.ContainsKey(obj))
        {
            foreach (GameObject go in _listObject[obj])
            {
                if (go.activeSelf) continue;
                return go;
            }
            GameObject g = Instantiate(obj, this.transform.position, Quaternion.identity);
            _listObject[obj].Add(g);
            g.SetActive(false);

            return g;
        }

        List<GameObject> list = new List<GameObject>();
        GameObject g2 = Instantiate(obj, this.transform.position, Quaternion.identity);
        list.Add(g2);
        g2.SetActive(false);
        _listObject.Add(obj, list);

        return g2;

    }
}
