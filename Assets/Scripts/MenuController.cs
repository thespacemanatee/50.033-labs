using UnityEngine;

public class MenuController : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 0.0f;
    }

    public void StartButtonClicked()
    {
        foreach (Transform eachChild in transform)
        {
            if (eachChild.name == "Score") continue;
            Debug.Log($"Child found. Name: {eachChild.name}");
            // disable them
            eachChild.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }
}