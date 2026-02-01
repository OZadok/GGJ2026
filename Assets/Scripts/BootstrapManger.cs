using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BootstrapManger: MonoBehaviour
{
    public static BootstrapManger Instance { get; private set; }

    [SerializeField] private SpriteRenderer backgroundRenderer;
    [SerializeField] private AudioSource backgroundMusic;
    
    private List<GameObject> _gameObjects = new();
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void ClearLevel()
    {
        _gameObjects.ForEach(Destroy);
        _gameObjects.Clear();
    }
    
    public void SetupLevel(LevelData level)
    {
        if (level == null)
        {
            Debug.LogError("Level missing");
            return;
        }

        ClearLevel();
        
        backgroundRenderer.sprite = level.bgImage;
        backgroundMusic.clip = level.backgroundMusic;
        backgroundMusic.Play();

        foreach (var zone in level.zones)
        {
            var zoneGameObject = Instantiate(zone.zonePrefab, zone.position, Quaternion.identity);
            zoneGameObject.GetComponent<Zone>().Init(zone.zoneData, zone.position);
            _gameObjects.Add(zoneGameObject);
        }

        foreach (var dispenserData in level.dispenserData)
        {
            var dispenserGameObject = Instantiate(dispenserData.prefab, dispenserData.position, Quaternion.identity);
            dispenserGameObject.GetComponent<Dispenser>().Init(dispenserData);
            _gameObjects.Add(dispenserGameObject);
        }
    }
}
