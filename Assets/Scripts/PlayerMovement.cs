using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 _moveDirection;
    private void OnMove(InputValue value)
    {
        _moveDirection = value.Get<Vector2>();
        
    }

    private void FixedUpdate()
    {
        transform.Translate(_moveDirection * Time.deltaTime);
    }
}
