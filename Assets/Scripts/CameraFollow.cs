using UnityEngine;
using Mirror;

public class CameraFollow : NetworkBehaviour
{
    [SerializeField] private Vector3 _cameraOffset;

    private Transform _camera;


    private void Awake()
    {
        _camera = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (isLocalPlayer)
            _camera.position = transform.position + _cameraOffset;
    }
}
