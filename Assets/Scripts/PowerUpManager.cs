using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    public List<GameObject> powerUpIcons;
    private List<IConsumable> _powerUps;

    // Start is called before the first frame update
    private void Start()
    {
        _powerUps = new List<IConsumable>();
        foreach (var t in powerUpIcons)
        {
            t.SetActive(false);
            _powerUps.Add(null);
        }
    }

    public void AddPowerUp(Texture texture, int index, IConsumable i)
    {
        if (index >= powerUpIcons.Count) return;
        powerUpIcons[index].GetComponent<RawImage>().texture = texture;
        powerUpIcons[index].SetActive(true);
        _powerUps[index] = i;
    }

    private void RemovePowerUp(int index)
    {
        Debug.Log("Removing: " + index);
        if (index >= powerUpIcons.Count) return;
        powerUpIcons[index].SetActive(false);
        _powerUps[index] = null;
    }

    private void Cast(int i, GameObject p)
    {
        if (_powerUps[i] == null) return;
        _powerUps[i].ConsumedBy(p); // interface method
        RemovePowerUp(i);
    }

    public void ConsumePowerUp(KeyCode k, GameObject player)
    {
        switch (k)
        {
            case KeyCode.Z:
                Cast(0, player);
                break;
            case KeyCode.X:
                Cast(1, player);
                break;
        }
    }
}