using System;
using System.Collections;
using UnityEngine;

public class NpcScript : MonoBehaviour
{
    private Group _group;
    
    private EntityScript _entityScript;

    private void Awake()
    {
        _entityScript = GetComponent<EntityScript>();
    }

    public void Init(SpawnPoint spawnPoint, Group group)
    {
        _group = group;
        StartCoroutine(DoActions());
    }

    private IEnumerator DoActions()
    {
        while (true)
        {
            for (int i = 0; i < _group.actions.Count; i++)
            {
                var waitTime = UnityEngine.Random.Range(_group.actions[i].minTime, _group.actions[i].maxTime);
                yield return new WaitForSeconds(waitTime);
                switch (_group.actions[i].type)
                {
                    case ActionsType.Drink:
                        _entityScript.DoAction1();
                        break;
                    case ActionsType.PunchTable:
                        _entityScript.DoAction2();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            yield return null;
        }
        // ReSharper disable once IteratorNeverReturns
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void OnSuspicious(bool isSuspicious)
    {
        Debug.Log($"isSuspicious: {isSuspicious}");
    }
}
