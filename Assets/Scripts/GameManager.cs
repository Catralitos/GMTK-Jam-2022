using Audio;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool fullscreen = false;
    public bool mouseControls = true;

    [HideInInspector] public int score;
    
    Resolution resolution;

    private AudioManager _audioManager;

    #region SingleTon

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // Needed if we want the audio manager to persist through scenes
        if (Instance == null)
        {
            Instance = this;
            _audioManager = AudioManager.Instance;
            resolution = Screen.currentResolution;
            fullscreen = Screen.fullScreen;
            UpdateDisplay();
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    #endregion

    public void ToggleFullscreen(bool value)
    {
        fullscreen = value;
        UpdateDisplay();
    }

    public void SetResolution(Resolution newResolution){
        resolution = newResolution;
        UpdateDisplay();
    }

    void UpdateDisplay() {
        Screen.SetResolution(resolution.width, resolution.height, fullscreen, 60);
    }
}