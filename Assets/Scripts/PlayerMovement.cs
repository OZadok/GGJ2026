using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 _moveDirection;
    private void OnMove(InputValue value)
    {
        var moveDirection2D = value.Get<Vector2>();
        _moveDirection = new Vector3(moveDirection2D.x, 0, moveDirection2D.y);
    }

    private void FixedUpdate()
    {
        transform.Translate(_moveDirection * Time.deltaTime);
    }
}
