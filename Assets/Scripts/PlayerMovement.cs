using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 _moveDirection;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        var targetMovement = _moveDirection * speed;
        var currentVelocity = _rigidbody2D.linearVelocity;
        var difference = targetMovement - currentVelocity;
        _rigidbody2D.AddForce(difference);
    }
}
