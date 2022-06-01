using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

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