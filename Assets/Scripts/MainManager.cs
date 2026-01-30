using System;
using System.Collections;
using UnityEngine;

public class MainManager: MonoBehaviour
{
    public event Action<int> OnTimerUpdated;
    public static MainManager Instance { get; private set; }
    
    [SerializeField] private int level = 0;
    [SerializeField] private int levelTimeSeconds = 30;

    private Coroutine _levelTimerCoroutine;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        StartLevel();
    }
    
    private void StartLevel()
    {
        if (_levelTimerCoroutine != null)
            StopCoroutine(_levelTimerCoroutine);

        BootstrapManger.Instance.SetupLevel(level);
            _levelTimerCoroutine = StartCoroutine(LevelCountDown());
    }
    
    IEnumerator LevelCountDown()
    {
        for (int i = 10; i > 0; i--)
        {
            OnTimerUpdated?.Invoke(i);
            yield return new WaitForSeconds(1f);
        }

        InvokePursuer();
    }

    private void InvokePursuer()
    {
        // check player
        // if failed game over
        // if clear, level++, StartLevel
    }
}