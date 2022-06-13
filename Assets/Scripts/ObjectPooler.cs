using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ObjectType
{
    GoombaEnemy = 0,
    GreenEnemy = 1
}

[System.Serializable]
public class ObjectPoolItem
{
    public int amount;
    public GameObject prefab;
    public bool expandPool;
    public ObjectType type;
}

public class ExistingPoolItem
{
    public readonly GameObject gameObject;
    public ObjectType type;

    // constructor
    public ExistingPoolItem(GameObject gameObject, ObjectType type)
    {
        // reference input
        this.gameObject = gameObject;
        this.type = type;
    }
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler sharedInstance;
    public List<ObjectPoolItem> itemsToPool; // types of different object to pool
    public List<ExistingPoolItem> pooledObjects; // a list of all objects in the pool, of all types

    private void Awake()
    {
        sharedInstance = this;
        pooledObjects = new List<ExistingPoolItem>();
        foreach (var item in itemsToPool)
        {
            for (var i = 0; i < item.amount; i++)
            {
                // this 'pickup' a local variable, but Unity will not remove it since it exists in the scene
                var pickup = Instantiate(item.prefab, transform, true);
                pickup.SetActive(false);
                pooledObjects.Add(new ExistingPoolItem(pickup, item.type));
            }
        }
    }

    public GameObject GetPooledObject(ObjectType type)
    {
        // return inactive pooled object if it matches the type
        foreach (var t in pooledObjects.Where(t => !t.gameObject.activeInHierarchy  &&  t.type  ==  type))
        {
            return  t.gameObject;
        }
        // this will be called when no more active object is present, item to expand pool if required
        foreach (var item in itemsToPool)
        {
            if (item.type != type || !item.expandPool) continue;
            var pickup = Instantiate(item.prefab, transform, true);
            pickup.SetActive(false);
            pooledObjects.Add(new ExistingPoolItem(pickup, item.type));
            return pickup;
        }
        return null;
    }
}