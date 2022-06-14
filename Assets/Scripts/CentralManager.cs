using UnityEngine;

// this has methods callable by players
public class CentralManager : MonoBehaviour
{
    public GameObject gameManagerObject;
    private GameManager _gameManager;
    public static CentralManager centralManagerInstance;

    private void Awake()
    {
        centralManagerInstance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _gameManager = gameManagerObject.GetComponent<GameManager>();
    }

    public void IncreaseScore()
    {
        _gameManager.IncreaseScore();
    }

    public static void DamagePlayer()
    {
        GameManager.DamagePlayer();
    }
}