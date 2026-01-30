using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Group", menuName = "Group", order = 0)]
public class Group : ScriptableObject
{
    public List<Item> items;
    public List<Action> actions;
}

[System.Serializable]
public struct Action
{
    public ActionsType type;
    public float minTime;
    public float maxTime;
}
public enum ActionsType
{
    Drink,
    PunchTable
}

