using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text score;
    private int _playerScore;

    public void IncreaseScore()
    {
        _playerScore += 1;
        score.text = "SCORE: " + _playerScore;
    }

    public void DamagePlayer(Vector3 location)
    {
        EventManager.TriggerEvent("OnPlayerDeath", new Dictionary<string, object> { { "location", location } });
        StartCoroutine(EndGame());
    }

    private static IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1.0f);
        EventManager.TriggerEvent("EndGame", null);
    }
}