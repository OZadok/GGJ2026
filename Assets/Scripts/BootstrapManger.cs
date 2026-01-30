using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BootstrapManger: MonoBehaviour
{
    public static BootstrapManger Instance { get; private set; }

    [SerializeField] private SpriteRenderer backgroundRenderer;
    [SerializeField] private AudioSource backgroundMusic;
    
    private List<LevelData> _levels;
    private List<GameObject> _zoneGameObjects = new();
    
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
        
        _zoneGameObjects.ForEach(Destroy);
        _zoneGameObjects.Clear();
        
        var currentLevel = _levels[level];
        
        backgroundRenderer.sprite = currentLevel.bgImage;
        backgroundMusic.clip = currentLevel.backgroundMusic;

        foreach (var zone in currentLevel.zones)
        {
            var zoneGameObject = Instantiate(zone.zonePrefab, zone.position, Quaternion.identity);
            zoneGameObject.GetComponent<Zone>().Init(zone.zoneData, zone.position);
            _zoneGameObjects.Add(zoneGameObject);
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
