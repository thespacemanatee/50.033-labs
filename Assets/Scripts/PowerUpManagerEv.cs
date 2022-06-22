using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PowerUpIndex
{
    OrangeMushroom = 0,
    BlueMushroom = 1
}

public class PowerUpManagerEv : MonoBehaviour
{
    // reference of all player stats affected
    public IntVariable marioJumpSpeed;
    public IntVariable marioMaxSpeed;
    public PowerUpInventory powerUpInventory;
    public List<GameObject> powerUpIcons;

    private void Start()
    {
        if (!powerUpInventory.gameStarted)
        {
            powerUpInventory.gameStarted = true;
            powerUpInventory.Setup(powerUpIcons.Count);
            ResetPowerUp();
        }
        else
        {
            // re-render the contents of the powerup from the previous time
            for (var i = 0; i < powerUpInventory.items.Count; i++)
            {
                var p = powerUpInventory.Get(i);
                if (p != null)
                {
                    AddPowerUpUI(i, p.powerUpTexture);
                }
            }
        }
    }

    public void ResetPowerUp()
    {
        foreach (var t in powerUpIcons)
        {
            t.SetActive(false);
        }
    }

    private void AddPowerUpUI(int index, Texture t)
    {
        powerUpIcons[index].GetComponent<RawImage>().texture = t;
        powerUpIcons[index].SetActive(true);
    }

    public void AddPowerUp(PowerUp p)
    {
        powerUpInventory.Add(p, (int)p.index);
        AddPowerUpUI((int)p.index, p.powerUpTexture);
    }

    private void ResetValues()
    {
        powerUpInventory.Clear();
    }

    public void OnApplicationQuit()
    {
        ResetValues();
    }
}