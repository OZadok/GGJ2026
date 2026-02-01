using System.Collections.Generic;
using System.Linq;
using SuperMaxim.Messaging;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }

    [SerializeField] private int level = 0;
    public PlayerScript playerPrefab;
    [SerializeField] private GameOverPanel gameOverPanel;
    [SerializeField] private LevelClearedPanel levelClearedPanel;
    [SerializeField] private GameObject gameCompletedPanel; 
    public float RemainingTime => _remainingTime;
    public float LevelTimeSeconds => CurrentLevelData.levelTime;

    private LevelData CurrentLevelData => _levels[level];

    private float _remainingTime;
    private bool _timerActive;
    private List<LevelData> _levels;

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
        _levels = Resources.LoadAll<LevelData>("ScriptableObjects/Levels").ToList();
    }

    private void Start()
    {
        StartLevel();
    }

    private void Update()
    {
        if (!_timerActive)
            return;

        _remainingTime -= Time.deltaTime;

        if (_remainingTime <= 0f)
        {
            _timerActive = false;
            InvokePursuer();
        }
    }

    public Group GetGroupIsBlendingTo(EntityScript playerEntity)
    {
        var player = playerEntity.GetComponent<PlayerScript>();
        var existingGroups = GetUniqueGroupsInZone(player.GetCurrentZone().ZoneData);
        
        foreach (var group in existingGroups)
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

    public void ResetLevel()
    {
        StartLevel();
    }
    
    public void ResetGame()
    {
        level = 0;
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        if(level + 1  >= _levels.Count)
        {
            gameCompletedPanel.SetActive(true);
            BootstrapManger.Instance.ClearLevel();
            return;
        }
        
        level++;
        StartLevel();
    }

    public void SkipWait()
    {
        _timerActive = false;
        _remainingTime = 0f;
        InvokePursuer();
    }

    private void StartLevel()
    {
        playerPrefab.Reset();
        BootstrapManger.Instance.SetupLevel(CurrentLevelData);
        _remainingTime = CurrentLevelData.levelTime;
        _timerActive = true;
    }

    private void InvokePursuer()
    {
        Messenger.Default.Publish(new LevelEndEvent(!playerPrefab.IsSus));

        if (playerPrefab.IsSus)
        {
            gameOverPanel.gameObject.SetActive(true);
            return;
        }

        levelClearedPanel.Init(level);
    }

    public static List<Group> GetUniqueGroupsInZone(ZoneData zoneData)
    {
        var uniqueGroups = new HashSet<Group>();

        if (zoneData == null)
            return new List<Group>();

        foreach (var group in zoneData.npcGroups)
        {
            if (group != null)
                uniqueGroups.Add(group);
        }
        
        return new List<Group>(uniqueGroups);
    }
}