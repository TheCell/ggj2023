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
}
