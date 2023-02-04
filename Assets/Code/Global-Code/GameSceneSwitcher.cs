using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneSwitcher : MonoBehaviour
{
    public void SwitchToGameScene()
    {
        SceneManager.LoadScene(GameSceneConstants.GameScene);
    }

    public void SwitchToStartScene()
    {
        SceneManager.LoadScene(GameSceneConstants.StartScene);
    }

    public void SwitchToWinScene()
    {
        SceneManager.LoadScene(GameSceneConstants.WinScene);
    }

    public void SwitchToLooseScene()
    {
        SceneManager.LoadScene(GameSceneConstants.LooseScene);
    }
}
