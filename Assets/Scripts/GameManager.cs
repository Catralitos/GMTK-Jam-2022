using Audio;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool fullscreen = false;
    public bool mouseControls = true;

    [HideInInspector] public int score;
    
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
            if (fullscreen) Screen.SetResolution(1920, 1080, true, 60);
            else Screen.SetResolution(1280, 720, false, 60);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    #endregion

    public void ToggleFullscreen()
    {
        if (fullscreen)
        {
            Screen.SetResolution(1280, 720, false, 60);
            fullscreen = false;
        }
        else
        {
            Screen.SetResolution(1920, 1080, true, 60);
            fullscreen = true;
        }
    }
}