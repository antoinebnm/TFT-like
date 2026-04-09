using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ChampionSpawner spawner;
    public ChampionData[] pool;

    void Start()
    {
        // TEMP: spawn 3 champs
        for (int i = 0; i < 3; i++)
        {
            spawner.SpawnChampion(pool[i]);
        }
    }
}
