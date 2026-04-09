using UnityEngine;

public class BoardTile : MonoBehaviour, IUnitHolder
{
    public int x;
    public int y;

    public Champion champion;

    public bool IsOccupied() => champion != null;

    public void SetCoordinates(int x, int y)
    {
        this.x = x;
        this.y = y;
        name = $"Tile_{x}_{y}";
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void SetChampion(Champion champ)
    {
        champion = champ;
        champ.transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            champ.transform.position.z
        );
    }

    public Champion GetChampion()
    {
        return champion;
    }

    public void Clear()
    {
        champion = null;
    }
}
