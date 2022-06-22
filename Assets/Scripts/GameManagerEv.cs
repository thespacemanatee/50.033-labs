using UnityEngine;
using UnityEngine.Events;

public class GameManagerEv : MonoBehaviour
{
    public UnityEvent onApplicationExit;

    private void Start()
    {
        Application.targetFrameRate = 30;
    }

    private void OnApplicationQuit()
    {
        Debug.Log("invoking application exit");
        onApplicationExit.Invoke();
    }
}