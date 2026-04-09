using UnityEngine;

public static class HighlightManager
{
    private static IUnitHolder previewTile;

    public static void UpdateSnapPreview(Camera cam, LayerMask layerMask)
    {
        Ray ray = cam.ScreenPointToRay(MouseDetection.Instance.MousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
        {
            IUnitHolder tile = hit.collider.GetComponent<IUnitHolder>();
            if (tile != previewTile)
            {
                ClearHighlight();
                previewTile = tile;
                Highlight(tile);
            }
        }
        else
        {
            ClearHighlight();
            previewTile = null;
        }
    }

    public static void Highlight(IUnitHolder tile)
    {
        if (tile == null)
            return;

        Renderer renderer = tile.GetTransform().GetComponentInChildren<Renderer>();

        if (renderer != null)
            renderer.material.color = Color.green;
    }

    public static void ClearHighlight()
    {
        if (previewTile == null)
            return;

        Renderer renderer = previewTile.GetTransform().GetComponentInChildren<Renderer>();
        if (renderer != null)
            renderer.material.color = Color.white;

        previewTile = null;
    }
}
