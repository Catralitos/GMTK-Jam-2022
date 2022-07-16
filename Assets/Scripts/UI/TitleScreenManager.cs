using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TitleScreenManager : MonoBehaviour
{
    [Header("Screens")] public GameObject titleScreen;
    public GameObject optionsScreen;
    public GameObject storyScreen;
    public GameObject tutorialScreen;
    
    [Header("TitleScreen")] public Button playButton;
    public Button optionsButton;
    public Button exitButton;
    public Button storyButton;
    public Button tutorialButton;
    
    [Header("OptionsScreen")] public Button optionsBackButton;
    
    [Header("StoryScreen")] public Button storyBackButton;

    [Header("TutorialScreen")] public Button tutorialBackButton;

    // Start is called before the first frame update
    private void Start()
    {
        playButton.onClick.AddListener(LoadGame);
        optionsButton.onClick.AddListener(ShowOptions);
        storyButton.onClick.AddListener(ShowStory);
        tutorialButton.onClick.AddListener(ShowTutorial);
        exitButton.onClick.AddListener(ExitGame);
        optionsBackButton.onClick.AddListener(BackOutOptions);
        storyBackButton.onClick.AddListener(BackOutStory);
        tutorialBackButton.onClick.AddListener(BackOutTutorial);
    }

    private static void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    private void ShowOptions()
    {
        titleScreen.SetActive(false);
        optionsScreen.SetActive(true);
    }
    
    private void ShowStory()
    {
        titleScreen.SetActive(false);
        storyScreen.SetActive(true);
    }
    
    private void ShowTutorial()
    {
        titleScreen.SetActive(false);
        tutorialScreen.SetActive(true);
    }

    private static void ExitGame()
    {
        Application.Quit();
    }

    private void BackOutOptions()
    {
        titleScreen.SetActive(true);
        optionsScreen.SetActive(false);
    }

    private void BackOutStory()
    {
        titleScreen.SetActive(true);
        storyScreen.SetActive(false);
    }
    
    private void BackOutTutorial()
    {
        titleScreen.SetActive(true);
        tutorialScreen.SetActive(false);
    }
}
