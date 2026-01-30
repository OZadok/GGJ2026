using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    private Zone _zone;
    private EntityScript _entityScript;

    private void Awake()
    {
        _entityScript =  GetComponent<EntityScript>();
    }

    public void JoinZone(Zone zone)
    {
        Debug.Log("Join Zone " + zone.name);


        var wasBlend = MainManager.Instance.IsBlendingToGroup(_entityScript);
        zone.AlertNpcs(wasBlend);
        // zone.
        // put player in the position he needs to be?
        // check for valid group.
        // if player is not in valid group
        //   zone.playerIsNotInValidGroup()
        // grace time...
        // wait for some action?
    }
}
