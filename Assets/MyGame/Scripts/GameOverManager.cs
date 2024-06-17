using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Button restartButton;

    void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
    }

    void RestartGame()
    {
        SceneManager.LoadScene("StartScene"); 
    }
}