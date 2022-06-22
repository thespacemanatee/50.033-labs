using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManagerEv : MonoBehaviour
{
    public GameConstants gameConstants;

    private void Start()
    {
        Debug.Log("SpawnManager start");
        for (var j = 0; j < 2; j++)
            SpawnFromPooler(ObjectType.GoombaEnemy);
    }

    private void StartSpawn(Scene scene, LoadSceneMode mode)
    {
        for (var j = 0; j < 2; j++)
            SpawnFromPooler(ObjectType.GoombaEnemy);
    }


    void SpawnFromPooler(ObjectType i)
    {
        var item = ObjectPooler.sharedInstance.GetPooledObject(i);

        if (item != null)
        {
            //set position
            item.transform.localScale = new Vector3(1, 1, 1);
            item.transform.position = new Vector3(Random.Range(-4.5f, 4.5f),
                gameConstants.groundSurface + item.GetComponent<SpriteRenderer>().bounds.extents.y, 0);
            item.SetActive(true);
        }
        else
        {
            Debug.Log("not enough items in the pool!");
        }
    }

    public void SpawnNewEnemy()
    {
        var i = Random.Range(0, 2) == 0 ? ObjectType.GoombaEnemy : ObjectType.GreenEnemy;
        SpawnFromPooler(i);
    }
}