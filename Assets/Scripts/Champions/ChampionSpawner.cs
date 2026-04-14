using UnityEngine;

public class ChampionSpawner : MonoBehaviour
{
    public GridManager gridManager;

    public bool SpawnChampion(ChampionData data)
    {
        BenchSlot slot = gridManager.GetFirstAvailableBenchTile();

        if (slot != null)
        {
            Vector3 position = slot.GetTransform().position;
            GameObject champObj = Instantiate(
                data.modelPrefab,
                new Vector3(position.x, position.y, data.modelPrefab.transform.position.z),
                data.modelPrefab.transform.rotation,
                transform
            );
            Champion champ = champObj.GetComponent<Champion>();

            champ.Init(data, slot);
            return true;
        }

        Debug.Log("Bench full");
        return false;
    }
}
