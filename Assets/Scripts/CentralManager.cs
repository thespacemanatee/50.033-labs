using UnityEngine;

// this has methods callable by players
public class CentralManager : MonoBehaviour
{
    public GameObject gameManagerObject;
    private GameManager _gameManager;
    public static CentralManager centralManagerInstance;

    // add reference to PowerUpManager
    public GameObject powerUpManagerObject;
    private PowerUpManager _powerUpManager;

    // add reference to SpawnManager
    public GameObject spawnManagerObject;
    private SpawnManager _spawnManager;

    private void Awake()
    {
        centralManagerInstance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _gameManager = gameManagerObject.GetComponent<GameManager>();
        _powerUpManager = powerUpManagerObject.GetComponent<PowerUpManager>();
        _spawnManager = spawnManagerObject.GetComponent<SpawnManager>();
    }

    public void IncreaseScore()
    {
        _gameManager.IncreaseScore();
    }

    public static void SpawnEnemy()
    {
        SpawnManager.Respawn(Random.Range(0, 2) == 0 ? ObjectType.GreenEnemy : ObjectType.GoombaEnemy);
    }

    public void DamagePlayer(Vector3 location)
    {
        _gameManager.DamagePlayer(location);
    }

    public void ConsumePowerUp(KeyCode k, GameObject g)
    {
        _powerUpManager.ConsumePowerUp(k, g);
    }

    public void AddPowerUp(Texture t, int i, IConsumable c)
    {
        _powerUpManager.AddPowerUp(t, i, c);
    }
}