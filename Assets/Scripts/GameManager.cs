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

    public static void DamagePlayer()
    {
        EventManager.TriggerEvent("OnPlayerDeath", null);
    }
}