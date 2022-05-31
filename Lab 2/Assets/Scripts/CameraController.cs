using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Mario's Transform
    public Transform endLimit; // GameObject that indicates end of map
    public Transform startLimit; // GameObject that indicates start of map

    private float _offset; // initial x-offset between camera and Mario
    private float _startX; // smallest x-coordinate of the Camera
    private float _endX; // largest x-coordinate of the camera
    private float _viewportHalfWidth;

    // Start is called before the first frame update
    private void Start()
    {
        if (Camera.main != null)
        {
            // get coordinate of the bottomleft of the viewport
            // z doesn't matter since the camera is orthographic
            var bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
            var position = transform.position;
            _viewportHalfWidth = Mathf.Abs(bottomLeft.x - position.x);

            _offset = position.x - player.position.x;
        }

        _endX = endLimit.transform.position.x - _viewportHalfWidth;
        _startX = startLimit.transform.position.x + _viewportHalfWidth;
    }

    // Update is called once per frame
    private void Update()
    {
        var desiredX = player.position.x + _offset;
        var position = transform.position;
        // check if desiredX is within startX and endX
        if (desiredX > _startX && desiredX < _endX) transform.position = new Vector3(desiredX, position.y, position.z);
    }
}