using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    private static readonly int Next = Animator.StringToHash("Next");
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame
            || Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            _animator.SetTrigger(Next);
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Intro 4"))
            {
                SceneManager.LoadScene($"SampleScene");
            }
        }
    }
}
