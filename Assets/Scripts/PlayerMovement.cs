using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector2 _moveDirection;
    private Rigidbody2D _rigidbody2D;
    private EntityScript _entityScript;

    private readonly Dictionary<SpriteRenderer, int> _baseOrders = new();

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _entityScript = GetComponent<EntityScript>();

        foreach (var sr in GetComponentsInChildren<SpriteRenderer>(true))
            _baseOrders[sr] = sr.sortingOrder;
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
        _rigidbody2D.AddForce(difference, ForceMode2D.Impulse);

        _entityScript.SetWalking(_rigidbody2D.linearVelocity.magnitude);

        SortingLayerUtil.SetDynamicSortingLayers(GetComponentsInChildren<SpriteRenderer>(true).ToList(), _baseOrders);
    }
}