using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    private static readonly int Next = Animator.StringToHash("Next");
    private Animator _animator;

    [SerializeField] private Transform _anyKeyLable;
    [SerializeField] private float showAnyKeyAfter = 5f;

    private float _idleTimer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _anyKeyLable.gameObject.SetActive(false);
    }

    private void Update()
    {
        bool inputPressed =
            (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame) ||
            (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame);

        if (inputPressed)
        {
            _idleTimer = 0f;
            _anyKeyLable.gameObject.SetActive(false);

            _animator.SetTrigger(Next);

            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Intro 6"))
                SceneManager.LoadScene("SampleScene");

            return;
        }

        _idleTimer += Time.deltaTime;
        if (_idleTimer >= showAnyKeyAfter && !_anyKeyLable.gameObject.activeSelf)
            _anyKeyLable.gameObject.SetActive(true);
    }
}