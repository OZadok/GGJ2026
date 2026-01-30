using UnityEngine;

namespace Core
{
    public class EntityManager: MonoBehaviour
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

        public void SpawnNpc(SpawnPoint point, Group group)
        {
            var entity = Instantiate(npcPrefab, point.spawnPoint.position, Quaternion.identity); 
            entity.GetComponent<NpcScript>().Init(point, group);
        }
    }
}