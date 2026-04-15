using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    private string gameSceneName = "GameScene";

    public void OnClickStart()
    {
        SceneLoader.LoadScene(gameSceneName);
    }

    public void OnClickExit()
    {
        SceneLoader.QuitGame();
    }
}
