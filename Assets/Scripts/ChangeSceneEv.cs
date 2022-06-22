using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneEv : MonoBehaviour
{
    public AudioSource changeSceneSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            changeSceneSound.PlayOneShot(changeSceneSound.clip);
            StartCoroutine(ChangeScene("MarioLevel2"));
        }
    }

    private IEnumerator WaitSoundClip(string sceneName)
    {
        yield return new WaitUntil(() => !changeSceneSound.isPlaying);
        StartCoroutine(ChangeScene("MarioGameEVLevel2"));
    }

    private static IEnumerator ChangeScene(string sceneName)
    {
        var asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}