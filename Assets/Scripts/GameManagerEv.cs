using UnityEngine;
using UnityEngine.Events;

public class GameManagerEv : MonoBehaviour
{
    public UnityEvent onApplicationExit;

    private void OnApplicationQuit()
    {
        onApplicationExit.Invoke();
    }
}