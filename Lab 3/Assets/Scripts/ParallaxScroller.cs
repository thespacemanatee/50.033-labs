using UnityEngine;

public class ParallaxScroller : MonoBehaviour

{
    public Renderer[] layers;
    public float[] speedMultiplier;
    private float _previousXPositionMario;
    private float _previousXPositionCamera;
    public Transform mario;
    public Transform mainCamera;
    private float[] _offset;

    private void Start()
    {
        _offset = new float[layers.Length];
        for (var i = 0; i < layers.Length; i++)
        {
            _offset[i] = 0.0f;
        }

        _previousXPositionMario = mario.transform.position.x;
        _previousXPositionCamera = mainCamera.transform.position.x;
    }

    private void Update()
    {
        // if camera has moved
        if (Mathf.Abs(_previousXPositionCamera - mainCamera.transform.position.x) > 0.001f)
        {
            for (var i = 0; i < layers.Length; i++)
            {
                if (_offset[i] > 1.0f || _offset[i] < -1.0f)
                    _offset[i] = 0.0f; //reset offset
                var newOffset = mario.transform.position.x - _previousXPositionMario;
                _offset[i] += newOffset * speedMultiplier[i];
                layers[i].material.mainTextureOffset = new Vector2(_offset[i], 0);
            }
        }

        //update previous pos
        _previousXPositionMario = mario.transform.position.x;
        _previousXPositionCamera = mainCamera.transform.position.x;
    }
}