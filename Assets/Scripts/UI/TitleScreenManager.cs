using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TitleScreenManager : MonoBehaviour
{
    public Button playButton;
    // Start is called before the first frame update
    private void Start()
    {
        playButton.onClick.AddListener(LoadGame);
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
