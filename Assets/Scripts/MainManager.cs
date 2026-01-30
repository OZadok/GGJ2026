using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;

public class MainManager : MonoBehaviour
{ 
    public event Action<int, int> OnTimerUpdated;
    public static MainManager Instance { get; private set; }

    [SerializeField] private int level = 0;
    [SerializeField] private int levelTimeSeconds = 30;
    [SerializeField] private PlayerScript playerPrefab;
    [SerializeField] private GameOverPanel gameOverPanel;
    [SerializeField] private LevelClearedPanel levelClearedPanel;
    private Coroutine _levelTimerCoroutine;
    private List<Group> _groups;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Initialize();
    }

    private void Initialize()
    {
        _groups = Resources.LoadAll<Group>("ScriptableObjects/Groups").ToList();
    }

    private void Start()
    {
        StartLevel();
    }

    public Group GetGroupIsBlendingTo(EntityScript playerEntity)
    {
        foreach (var group in _groups)
        {
            if (group.items.Count != playerEntity.GetItemsCount())
            {
                continue;
            }

            bool wasBroke = false;
            foreach (var item in group.items)
            {
                if (!playerEntity.items.ContainsKey(item.itemType))
                {
                    wasBroke = true;
                    break;
                }
            }
            if (wasBroke)
            {
                continue;
            }
            return group;
        }
        return null;
    }

    public void Reset()
    {
        level = 0;
        StartLevel();
    }

    public void NextLevel()
    {
        Debug.Log($"Level {level} cleared!");
        level++;
        StartLevel();
    }
    public void SkipWait()
    {
        _timerActive = false;
        _remainingTime = 0f;
        InvokePursuer();
        OnTimerUpdated?.Invoke(0f, levelTimeSeconds);
    }
    private void StartLevel()
    {
        BootstrapManger.Instance.SetupLevel(level);
        _remainingTime = levelTimeSeconds;
        _timerActive = true;
        OnTimerUpdated?.Invoke(_remainingTime, levelTimeSeconds);
    }

    private void InvokePursuer()
    {
        if (playerPrefab.IsSus)
        {
            gameOverPanel.gameObject.SetActive(true);
            return;
        }

        levelClearedPanel.Init(level);
    }
}