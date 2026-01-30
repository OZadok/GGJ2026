using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 _moveDirection;
    public void OnMove(InputAction.CallbackContext context)
    {
        var moveDirection2D = context.ReadValue<Vector2>();
        _moveDirection = new Vector3(moveDirection2D.x, moveDirection2D.y, 0);
    }

    private void FixedUpdate()
    {
        transform.Translate(_moveDirection * Time.deltaTime);
    }
}
