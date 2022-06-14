using UnityEngine;

public class Rotator : MonoBehaviour
{
    public GameConstants gameConstants;
    private Vector3 _rotator;

    // Start is called before the first frame update
    private void Start()
    {
        _rotator = new Vector3(0, gameConstants.rotatorRotateSpeed, 0);
    }

    // Update is called once per frame
    private void Update()
    {
        //Rotate
        transform.rotation = Quaternion.Euler(transform.eulerAngles - _rotator);
    }
}