using UnityEngine;

public class BenchSlot : MonoBehaviour, IUnitHolder
{
    public int x;

    public Champion champion;

    public bool IsOccupied() => champion != null;

    public void SetCoordinates(int x)
    {
        this.x = x;
        name = $"BenchSlot_{x}";
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
