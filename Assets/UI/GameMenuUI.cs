using UnityEngine;

public class GameMenuUI : MonoBehaviour
{
    [SerializeField]
    private Canvas parentCanvas;

    public void OnClickResume()
    {
        parentCanvas.gameObject.SetActive(false);
    }

    public void OnClickExit()
    {
        SceneLoader.QuitGame();
    }
}
