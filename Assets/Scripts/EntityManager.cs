using UnityEngine;

namespace Core
{
    public class EntityManager : MonoBehaviour
    {
        public static EntityManager Instance { get; private set; }

        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject npcPrefab;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public NpcScript SpawnNpc(SpawnPoint point, Group group)
        {
            var entity = Instantiate(npcPrefab, point.spawnPoint);
            var npcScript = entity.GetComponent<NpcScript>();
            npcScript.Init(point, group);
            EquipmentAnchors anchors = entity.GetComponent<EquipmentAnchors>();
            foreach (var item in group.items)
            {
                SpawnItem(anchors, item);
            }

            return npcScript;
        }

        private void SpawnItem(EquipmentAnchors anchors, ItemData item)
        {
            Transform position = anchors.GetItemPosition(item.itemType);
            var itemGO = Instantiate(item.baseItemPrefab, position, false);
            itemGO.GetComponent<SpriteRenderer>().sprite = item.itemSprites[Random.Range(0,item.itemSprites.Count)];
        }
    }
}