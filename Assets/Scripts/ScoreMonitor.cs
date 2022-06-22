using UnityEngine.UI;
using UnityEngine;

public class ScoreMonitor : MonoBehaviour
{
    public IntVariable marioScore;
    public Text text;

    private void Start()
    {
        UpdateScore();
    }

    public void UpdateScore()
    {
        text.text = "Score: " + marioScore.Value;
    }
}