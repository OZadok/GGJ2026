using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public event Action<int, int> OnTimerUpdated;
    public static MainManager Instance { get; private set; }

    [SerializeField] private int level = 0;
    [SerializeField] private int levelTimeSeconds = 30;

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
                if (!playerEntity.items.ContainsKey(item.itemType) || playerEntity.items[item.itemType] != item)
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

    private void StartLevel()
    {
        if (_levelTimerCoroutine != null)
            StopCoroutine(_levelTimerCoroutine);

        BootstrapManger.Instance.SetupLevel(level);
        _levelTimerCoroutine = StartCoroutine(LevelCountDown());
    }

    IEnumerator LevelCountDown()
    {
        for (int i = levelTimeSeconds; i > 0; i--) // im guessing the 10 here was a placeholder?
        {
            OnTimerUpdated?.Invoke(i, levelTimeSeconds);
            yield return new WaitForSeconds(1f);// because we dont update the number inbetween seconds ui timer will not be smooth
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