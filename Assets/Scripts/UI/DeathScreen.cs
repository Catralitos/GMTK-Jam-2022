using Audio;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    public Button mainMenuButton;
    public TextMeshProUGUI statsBox;
    [HideInInspector]public string statsString;
    //private AudioManager _audioManager;

    void Start()
    {
        //_audioManager = GetComponent<AudioManager>();
        StatsCollector.PlayerStats stats = StatsCollector.GetStats();
        statsString = statsString + "smallEnemiesKilled: " + stats.smallEnemiesKilled +
            " largeEnemiesKilled: " + stats.largeEnemiesKilled + " baseEnemiesKilled: " +  stats.baseEnemiesKilled + 
            " totalExpObtained: " + stats.totalExpObtained +
            " wavesCleared: " + stats.wavesCleared + " timeSurvived: " + stats.timeSurvived +
            " superBulletsShot: " + stats.superBulletsShot + 
            " knockbackBulletsShot: " + stats.knockbackBulletsShot + " multiBulletsShot: " + stats.multiBulletsShot + 
            " piercingBulletsShot: " + stats.piercingBulletsShot;
        statsBox.text = statsString;
        mainMenuButton.onClick.AddListener(BackToMainMenu);
    }

    void BackToMainMenu()
    {
        //_audioManager.Play("ButtonClick");
        SceneManager.LoadScene(0);
    }
    
}


