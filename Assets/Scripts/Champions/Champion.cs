using UnityEngine;

public class Champion : MonoBehaviour
{
    public ChampionData data;

    public float currentHealth;
    public IUnitHolder currentTile;

    public void Init(ChampionData data)
    {
        this.data = data;
        currentHealth = data.baseHealth;
    }

    public void SetTile(IUnitHolder tile)
    {
        if (tile == null || tile.GetTransform() == null)
        {
            Debug.LogError("Trying to assign destroyed tile!");
            return;
        }

        currentTile = tile;
        currentTile.SetChampion(this);
    }
}
