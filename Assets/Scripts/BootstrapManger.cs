using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BootstrapManger: MonoBehaviour
{
    public static BootstrapManger Instance { get; private set; }

    [SerializeField] private GameObject zonePrefab;
    [SerializeField] private SpriteRenderer backgroundRenderer;
    [SerializeField] private AudioSource backgroundMusic;
    
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
    
    public void SetupLevel(int level)
    {
        if (level >= _levels.Count)
        {
            Debug.LogError($"Level {level} not exists");
            return;
        }
        
        var currentLevel = _levels[level];
        
        backgroundRenderer.sprite = currentLevel.bgImage;
        backgroundMusic.clip = currentLevel.backgroundMusic;

        foreach (var zone in currentLevel.zoneData)
        {
            var zoneGameObject = Instantiate(zonePrefab, zone.zonePostion, Quaternion.identity);
            zoneGameObject.GetComponent<Zone>().Init(zone);
        }

        foreach (var dispenserData in currentLevel.dispenserData)
        {
            var dispenserGameObject = Instantiate(dispenserData.prefab, dispenserData.position, Quaternion.identity);
            dispenserGameObject.GetComponent<Dispenser>().Init(dispenserData);
        }
    }

    private void Initialize()
    {
        _levels = Resources.LoadAll<LevelData>("ScriptableObjects/Levels").ToList();
    }
}
