using Audio;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    public Button mainMenuButton;
    public Button restarButton;
    public Button creditsButton;
    public TextMeshProUGUI statsBox;
    [HideInInspector]public string statsString;
    //private AudioManager _audioManager;

    void Start()
    {
        //_audioManager = GetComponent<AudioManager>();
        StatsCollector.PlayerStats stats = StatsCollector.GetStats();
        statsString = statsString + "Small Enemies Killed: " + stats.smallEnemiesKilled +
            "\nLarge Enemies Killed: " + stats.largeEnemiesKilled + "\nBase Enemies Killed: " +  stats.baseEnemiesKilled + 
            "\nTotal Exp Obtained: " + stats.totalExpObtained +
            "\nWaves Cleared: " + stats.wavesCleared + "\nTime Survived: " + stats.timeSurvived +
            "\nSuper Bullets Shot: " + stats.superBulletsShot + 
            "\nKnockback Bullets Shot: " + stats.knockbackBulletsShot + "\nMulti Bullets Shot: " + stats.multiBulletsShot + 
            "\nPiercing Bullets Shot: " + stats.piercingBulletsShot;
        statsBox.text = statsString;
        mainMenuButton.onClick.AddListener(BackToMainMenu);
        restarButton.onClick.AddListener(Restart);
        creditsButton.onClick.AddListener(Credits);
    }

    void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

     void Credits()
    {
        SceneManager.LoadScene(4);
    }
    
   void Restart()
    {
        SceneManager.LoadScene(1);
    }
}


