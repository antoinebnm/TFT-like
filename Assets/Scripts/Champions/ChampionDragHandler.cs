using UnityEngine;

[RequireComponent(typeof(Champion))]
public class ChampionDragHandler : MonoBehaviour
{
    private Champion champion;

    private Camera cam;

    private bool isDragging = false;
    private Vector3 offset;
    private float dragPlaneZ;

    private IUnitHolder originHolder;

    // smoothing
    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.02f;

    private int boardLayerMask;

    [SerializeField]
    private GridManager gridManager;

    private void Awake()
    {
        champion = GetComponent<Champion>();
        boardLayerMask = LayerMask.GetMask("Board");
    }

    public void StartDrag(Camera camera, Vector2 mousePos)
    {
        cam = camera;
        if (cam == null)
        {
            cam = Camera.main;
            if (cam == null)
                return;
        }

        originHolder = champion.GetCurrentTile();
        isDragging = true;

        Debug.Log(originHolder.GetTransform().name);

        dragPlaneZ = transform.position.z;

        Vector3 world = ScreenToWorldOnPlane(mousePos);
        offset = transform.position - world;
    }

    public void ForceStopDrag()
    {
        if (!isDragging)
            return;
        isDragging = false;
        TryPlace();
    }

    private void Update()
    {
        if (!isDragging)
            return;
        if (MouseDetection.Instance == null || cam == null)
            return;

        Vector2 mousePos = MouseDetection.Instance.MousePosition;
        Vector3 target = ScreenToWorldOnPlane(mousePos) + offset;

        // smooth movement to avoid jitter
        transform.position = Vector3.SmoothDamp(
            transform.position,
            target,
            ref velocity,
            smoothTime
        );
        HighlightManager.UpdateSnapPreview(cam, boardLayerMask);
    }

    private Vector3 ScreenToWorldOnPlane(Vector2 mousePos)
    {
        Ray ray = cam.ScreenPointToRay(mousePos);
        Plane plane = new Plane(Vector3.forward, new Vector3(0, 0, dragPlaneZ));
        if (plane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        return transform.position;
    }

    void TryPlace()
    {
        if (MouseDetection.Instance == null || cam == null)
        {
            ResetPosition();
            return;
        }

        Ray ray = cam.ScreenPointToRay(MouseDetection.Instance.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, boardLayerMask))
        {
            IUnitHolder targetTile = hit.collider.GetComponent<IUnitHolder>();

            if (targetTile != null)
            {
                if (!targetTile.IsOccupied())
                    MoveTo(targetTile);
                else
                    SwapChampions(targetTile);
            }
            HighlightManager.ClearHighlight();
            return;
        }

        ResetPosition();
    }

    void SwapChampions(IUnitHolder targetTile)
    {
        // Champion on the target tile
        Champion targetChamp = targetTile.GetChampion();
        // Origin tile/position of the dragged champion
        IUnitHolder origin = champion.GetCurrentTile();

        if (targetChamp == null)
            return;

        targetTile.Clear();
        origin.Clear();

        targetChamp.SetTile(origin);
        champion.SetTile(targetTile);
    }

    void MoveTo(IUnitHolder targetTile)
    {
        IUnitHolder currentTile = champion.GetCurrentTile();
        if (currentTile != null)
            currentTile.Clear();

        champion.SetTile(targetTile);
    }

    void ResetPosition()
    {
        if (originHolder == null)
            return;

        if (originHolder is IUnitHolder holder && holder.GetTransform() != null)
        {
            champion.SetTile(holder);
        }
        else
        {
            Debug.LogWarning("Origin holder is invalid or destroyed during reset.");
        }

        HighlightManager.ClearHighlight();
    }
}
