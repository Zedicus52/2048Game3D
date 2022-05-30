using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class PoolBasic<T> where T : MonoBehaviour
{ 
    public T Prefab { get; private set; }
    public int BasicCount { get; private set; }
    public bool AutoExpand { get; private set; }
    public Transform Parent { get; private set; }

    private List<T> _objects;

    public PoolBasic(T prefab, bool autoExpand)
    {
        Prefab = prefab;
        AutoExpand = autoExpand;
        CreatePool();
    }
    public PoolBasic(T prefab, bool autoExpand, int count) : this(prefab, autoExpand)
    {
        BasicCount = count;
    }
    public PoolBasic(T prefab, bool autoExpand, int count, Transform parent) : this(prefab, autoExpand, count)
    {
        Parent = parent;
    }

    private void CreatePool()
    {
        _objects = new List<T>(BasicCount);
        for (int i = 0; i < BasicCount; i++)
            CreateObject();
    }

    private T CreateObject()
    {
        var obj = Object.Instantiate(Prefab, Parent);
        obj.gameObject.SetActive(false);
        _objects.Add(obj);
        return obj;
    }

    private bool HasFreeElement(out T element)
    {
        foreach (var obj in _objects)
        {
            if (obj.isActiveAndEnabled == false)
            {
                element = obj;
                obj.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out var element))
            return element;
        if (AutoExpand)
            return CreateObject();

        throw new Exception("No free elements");
    }
}
