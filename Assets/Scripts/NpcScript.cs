using System;
using System.Collections;
using SuperMaxim.Messaging;
using UnityEngine;
using Random = UnityEngine.Random;

public class NpcScript : MonoBehaviour
{
    [SerializeField] SpriteRenderer reactionRenderer;
    [SerializeField] Sprite badReaction;
    [SerializeField] Sprite goodReaction;
    [SerializeField] float reactionStayTime = 5f;
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
                    case ActionsType.None:
                        break;
                    case ActionsType.Drink:
                        _entityScript.DoAction1();
                        break;
                    case ActionsType.PunchTable:
                        _entityScript.DoAction2();
                        break;
                    case ActionsType.Hurray:
                        _entityScript.DoAction3();
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
        StartCoroutine(OnSuspiciousDelay(isSuspicious));
        
    }

    private IEnumerator OnSuspiciousDelay(bool isSuspicious)
    {
        var delay = Random.Range(0, 0.5f);
        yield return new WaitForSeconds(delay);
        UpdateReacionSprite(isSuspicious);
        Debug.Log($"isSuspicious: {isSuspicious}");
        Messenger.Default.Publish(new NpcSuspiciousEvent(isSuspicious));
    }

    private void UpdateReacionSprite(bool isSuspicious)
    {
        reactionRenderer.gameObject.SetActive(true);
        reactionRenderer.sprite = isSuspicious ? badReaction : goodReaction;
        Invoke(nameof(DisableReactionSprite), reactionStayTime);
    }
    void DisableReactionSprite() => reactionRenderer.gameObject.SetActive(false);
}
