using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolView : MonoBehaviour
{
    private Transform PoolParent;
    private Dictionary<string, List<GameObject>> pool = new Dictionary<string, List<GameObject>>();
    public void Create(string name, GameObject gameObject, int size)
    {
        if(PoolParent == null)
        {
            PoolParent = new GameObject("Pool").transform;
        }
        Transform ObjectsParent = new GameObject(name).transform;
        ObjectsParent.parent = PoolParent;
        List<GameObject> newPool = new List<GameObject>();

        for (int i = 0; i < size; i++)
        {
            var @object = gameObject.Instantiate(ObjectsParent);
            @object.SetActive(false);
            newPool.Add(@object);
        }

        pool.Add(name, newPool);
    }

    public bool Get(string name, out GameObject @object)
    {
        @object = null;
        foreach (var item in pool[name])
        {
            if (!item.activeSelf)
            {
                @object = item;
                return true;
            }
        }
        return false;
    }
}
