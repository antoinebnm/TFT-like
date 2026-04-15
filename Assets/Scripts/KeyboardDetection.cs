using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardDetection : MonoBehaviour
{
    public static KeyboardDetection Instance;
    public Camera PrimaryCamera;
    public Canvas menuCanva;
    public ChampionSpawner spawner;
    public ChampionData exampleSpawnableUnitData;

    protected void Awake()
    {
        Instance = this;
        if (PrimaryCamera == null)
            PrimaryCamera = Camera.main;
    }

    void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Vector2 MousePosition = Mouse.current.position.ReadValue();

            Ray ray = PrimaryCamera.ScreenPointToRay(MousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Champion champion = hit.collider.GetComponent<Champion>();
                if (champion != null)
                    champion.SellChampion();
            }
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            spawner.SpawnChampion(exampleSpawnableUnitData);
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            menuCanva.gameObject.SetActive(true);
        }
    }
}
