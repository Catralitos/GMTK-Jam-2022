using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsManager : MonoBehaviour
{

    public Button titleButton;
    public Button exitButton;

    private void Start()
    {
        titleButton.onClick.AddListener(GoToTitle);
        exitButton.onClick.AddListener(ExitGame);
    }

    private static void GoToTitle()
    {
        SceneManager.LoadScene(0);
    }

    private static void ExitGame()
    {
        Application.Quit();
    }
}
