using UnityEngine;

public interface IUnitHolder
{
    bool IsOccupied();
    void SetChampion(Champion champ);
    Champion GetChampion();
    void Clear();
    Transform GetTransform();
}
