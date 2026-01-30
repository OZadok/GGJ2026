using System;
using System.Collections;
using UnityEngine;

public class NpcScript : MonoBehaviour
{
    [SerializeField] private Group _group;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        //set wearables
        //set items
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
                //do action from type - _group.actions[i].type;
            }
            yield return null;
        }
        // ReSharper disable once IteratorNeverReturns
    }

    private void OnSuspicious(bool isSuspicious)
    {
        
    }
}
