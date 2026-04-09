using UnityEngine;
using UnityEngine.InputSystem;

public class MouseDetection : MonoBehaviour
{
    public static MouseDetection Instance;

    public Camera PrimaryCamera;
    public Vector2 MousePosition { get; private set; }

    protected void Awake()
    {
        Instance = this;
        if (PrimaryCamera == null)
            PrimaryCamera = Camera.main;
    }

    void Update()
    {
        if (Mouse.current == null || PrimaryCamera == null)
            return;

        MousePosition = Mouse.current.position.ReadValue();

        // Start drag on press
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = PrimaryCamera.ScreenPointToRay(MousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                ChampionDragHandler drag = hit.collider.GetComponent<ChampionDragHandler>();
                if (drag != null)
                {
                    drag.StartDrag(PrimaryCamera, MousePosition);
                }
            }
        }

        // End drag on release (global safety)
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            ChampionDragHandler[] all = FindObjectsByType<ChampionDragHandler>(
                FindObjectsSortMode.None
            );
            foreach (var d in all)
            {
                d.ForceStopDrag();
            }
        }
    }
}
