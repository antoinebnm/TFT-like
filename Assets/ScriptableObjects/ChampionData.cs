using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChampionData", menuName = "Scriptable Objects/ChampionData")]
public class ChampionData : ScriptableObject
{
    public string unitName;
    public int cost;
    public int level = 1;
    public float baseHealth;
    public float baseDamageResistance;
    public float baseAttackDamage;
    public float attackRange; // e.g., 1 for melee, 3 for ranged
    public GameObject modelPrefab;
    public List<string> traits; // "Cyborg", "Assassin", etc.
}
