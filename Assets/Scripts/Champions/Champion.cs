using UnityEngine;

public class Champion : MonoBehaviour
{
    public ChampionData data;

    public float currentHealth;
    private IUnitHolder currentTile;

    public void Init(ChampionData data, IUnitHolder slot)
    {
        this.data = data;
        currentHealth = data.baseHealth;
        SetTile(slot);
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
        Debug.Log(currentTile.GetTransform().name);
    }

    public IUnitHolder GetCurrentTile()
    {
        return currentTile;
    }

    public bool SellChampion()
    {
        GameObject go = gameObject;
        try
        {
            currentTile.Clear();
            Destroy(go);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
