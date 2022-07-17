using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenManager : MonoBehaviour
{
    public Button creditsButton;
    
    private void Start()
    {
        creditsButton.onClick.AddListener(LoadCredits);
    }

    private static void LoadCredits()
    {
        SceneManager.LoadScene(4);
    }
}
