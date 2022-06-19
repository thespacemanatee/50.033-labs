using UnityEngine;

[CreateAssetMenu(fileName = "IntVariable", menuName = "ScriptableObjects/IntVariable", order = 2)]
public class IntVariable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline] public string developerDescription = "";
#endif

    public int Value { get; private set; }

    public void SetValue(int value)
    {
        Value = value;
    }

    // overload
    public void SetValue(IntVariable value)
    {
        Value = value.Value;
    }

    public void ApplyChange(int amount)
    {
        Value += amount;
    }

    public void ApplyChange(IntVariable amount)
    {
        Value += amount.Value;
    }
}