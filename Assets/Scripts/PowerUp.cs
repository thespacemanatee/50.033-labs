using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "ScriptableObjects/PowerUp", order = 5)]
public class PowerUp : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline] public string developerDescription = "";
#endif
    // index in the UI
    public PowerUpIndex index;

    // texture in the UI
    public Texture powerUpTexture;

    // list of things any powerup can do
    public int absoluteSpeedBooster;
    public int absoluteJumpBooster;

    // effect of powerup
    public int duration;

    public List<int> Utilise()
    {
        return new List<int> { absoluteSpeedBooster, absoluteJumpBooster };
    }

    public void Reset()
    {
        absoluteSpeedBooster = 0;
        absoluteJumpBooster = 0;
    }

    public void Enhance(int speedBooster, int jumpBooster)
    {
        absoluteSpeedBooster += speedBooster;
        absoluteJumpBooster += jumpBooster;
    }
}