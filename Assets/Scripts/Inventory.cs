using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Inventory<T> : ScriptableObject
{
    public bool gameStarted;
    public List<T> items = new();

    public void Setup(int size)
    {
        for (var i = 0; i < size; i++)
        {
            items.Add(default);
        }
    }

    public void Clear()
    {
        items = new List<T>();
        gameStarted = false;
    }

    public void Add(T thing, int index)
    {
        if (index < items.Count)
            items[index] = thing;
    }

    public void Remove(int index)
    {
        if (index < items.Count)
            items[index] = default;
    }

    public T Get(int index)
    {
        return index < items.Count ? items[index] : default;
    }
}