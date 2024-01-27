using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField, Min(0)] private float _speed = 5;
    [SerializeField, Min(0)] private Vector3 _teleportPosition;

    private Rigidbody _rb;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        var moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Move(moveDirection);
        if (!isServer)
            CmdMove(moveDirection);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isServer)
                CmdTeleport();
            Teleport();
        }
    }

    private void Move(Vector3 direction)
    {

        _rb.velocity = _rb.velocity + direction * _speed;
        _rb.velocity = new Vector3(_rb.velocity.x / 2, _rb.velocity.y, _rb.velocity.z / 2);
    }


    private void Teleport()
    {
        transform.position = _teleportPosition;
    }

    [Command] private void CmdMove(Vector3 direction) => Move(direction);
    [Command] private void CmdTeleport() => Teleport();
}
