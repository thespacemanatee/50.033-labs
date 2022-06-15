using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 0.0f;
    }

    private void OnEnable()
    {
        EventManager.StartListening("EndGame", ResetGame);
    }

    private void OnDisable()
    {
        EventManager.StopListening("EndGame", ResetGame);
    }

    public void StartButtonClicked()
    {
        foreach (Transform eachChild in transform)
        {
            if (eachChild.name is "Score" or "PowerUps") continue;
            Debug.Log($"Child found. Name: {eachChild.name}");
            // disable them
            eachChild.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    private static void ResetGame(Dictionary<string, object> message)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}