using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    private static readonly int Next = Animator.StringToHash("Next");
    private Animator _animator;
    [SerializeField] private Transform _anyKeyLable;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame
            || Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            if (_anyKeyLable.gameObject.activeSelf) { _anyKeyLable.gameObject.SetActive(false); }

            _animator.SetTrigger(Next);
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Intro 6"))
            {
                SceneManager.LoadScene($"SampleScene");
            }
        }
    }
}
