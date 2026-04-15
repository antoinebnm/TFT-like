using UnityEngine;

// DEBUG: Remove late dev
[ExecuteAlways]
public class EnemyBoardManager : MonoBehaviour
{
    [Header("Grid Settings")]
    private readonly float hexRadius = 0.55f;

    [SerializeField]
    private GameObject hexPrefab;

    [SerializeField]
    private GameObject rectPrefab;

    // The 2D array to store our tiles
    private BoardTile[,] board;
    private BenchSlot[] bench;

    private float xSpacing;
    private float ySpacing;

    [SerializeField]
    [Min(0)]
    private float boardScaleFactor = 1.24f;

    [SerializeField]
    private float hexXSpacing = 1f;

    [SerializeField]
    private float hexYSpacing = 0.88f;

    [SerializeField]
    private float baseXWorldPos = -2.9f;

    [SerializeField]
    private float baseYWorldPos = 0.6f;

    [SerializeField]
    private float benchOffset = 0.8f;

    [SerializeField]
    private float benchXSpacing = -1.4f;

    [SerializeField]
    private float benchYSpacing = 1f;

    [SerializeField]
    [Min(0)]
    private float benchScaleFactor = 0.6f;

    [SerializeField]
    private bool isOdd = false;

    [Header("Debug")] // DEBUG: KEEP FALSE to avoid MissingReferenceException at Champion first drag
    public bool autoUpdate = false;

    [ContextMenu("Manually Rebuild Grid")]
    private void ManuallyRebuild()
    {
        RefreshGrid();
    }

    void Awake()
    {
        xSpacing = Mathf.Sqrt(3) * hexRadius;
        ySpacing = 1.5f * hexRadius;

        ClearExistingTiles();
        GenerateBoard();
        GenerateBench();
    }

    private void OnValidate()
    {
        if (
            // Application.isPlaying &&
            autoUpdate
            && hexPrefab != null
            && rectPrefab != null
        )
        {
            // We use UnityEditor.EditorApplication.delayCall to avoid
            // errors when destroying objects during OnValidate
#if UNITY_EDITOR
            UnityEditor.EditorApplication.delayCall += RefreshGrid;
#endif
        }
    }

    private void RefreshGrid()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.delayCall -= RefreshGrid;
        if (this == null)
            return;

        ClearExistingTiles();
        GenerateBoard();
        GenerateBench();
#endif
    }

    [ContextMenu("Clear Grid")]
    private void ClearExistingTiles()
    {
        // We delete children so we don't stack hexes on top of each other
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    void GenerateBoard()
    {
        board = new BoardTile[7, 4];

        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 7; x++)
            {
                // Calculate position
                float xPos = x * xSpacing;

                // Horizontal Offset: Shift odd rows to the right
                // y % 2 == 0 for even shift | y % 2 != 0 for odd shift
                if (isOdd)
                {
                    if (y % 2 != 0)
                        xPos += xSpacing / 2f;
                }
                else if (y % 2 == 0)
                {
                    xPos += xSpacing / 2f;
                }

                float yPos = y * ySpacing;

                // Instantiate and store in array
                Vector3 worldPos = new Vector3(
                    baseXWorldPos + (xPos * hexXSpacing),
                    baseYWorldPos + (yPos * hexYSpacing),
                    0
                );
                GameObject go = Instantiate(hexPrefab, worldPos, Quaternion.identity, transform);
                go.transform.localScale = new Vector3(boardScaleFactor, boardScaleFactor, 1);

                BoardTile tile = go.GetComponent<BoardTile>();
                tile.SetCoordinates(x, y);

                board[x, y] = tile;
            }
        }
    }

    void GenerateBench()
    {
        bench = new BenchSlot[9];
        float worldXPos = -benchXSpacing;
        float worldYPos = -benchYSpacing + (hexXSpacing * 4);

        for (int x = 0; x < 9; x++)
        {
            float xPos = x * benchOffset;
            xPos += (benchOffset / 2f) - 2;

            // Instantiate and store in array
            Vector3 worldPos = new Vector3(
                baseXWorldPos + worldXPos + xPos,
                baseYWorldPos + worldYPos,
                0
            );
            GameObject go = Instantiate(rectPrefab, worldPos, Quaternion.identity, transform);
            go.transform.localScale = new Vector3(benchScaleFactor, benchScaleFactor, 1);

            BenchSlot slot = go.GetComponent<BenchSlot>();
            slot.SetCoordinates(x);

            bench[x] = slot;
        }
    }

    public BoardTile GetTileAt(int x, int y)
    {
        if (x >= 0 && x < 7 && y >= 0 && y < 4)
        {
            return board[x, y];
        }
        return null;
    }

    public BenchSlot GetBenchTileAt(int x)
    {
        if (x >= 0 && x < 9)
        {
            return bench[x];
        }
        return null;
    }
}
