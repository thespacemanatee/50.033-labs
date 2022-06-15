using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private void Awake()
    {
        // spawn two gombaEnemy
        for (var j = 0; j < 2; j++)
        {
            SpawnFromPooler(ObjectType.GoombaEnemy, 0);
        }
    }

    public static void Respawn(ObjectType i)
    {
        SpawnFromPooler(i, 10f);
    }

    static void SpawnFromPooler(ObjectType i, float y)
    {
        // static method access
        var item = ObjectPooler.sharedInstance.GetPooledObject(i);
        if (item != null)
        {
            //set position, and other necessary states
            item.transform.position = new Vector3(Random.Range(-4.5f, 4.5f), y == 0 ? item.transform.position.y : y, 0);
            item.SetActive(true);
        }
        else
        {
            Debug.Log("not enough items in the pool.");
        }
    }
}