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
        Debug.Log("updating score");
        text.text = "Score: " + marioScore.Value;
    }
}