using Player;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Health")] public Image healthBar;

    [Header("Buffs")] public Image superBulletIcon;
    public TextMeshProUGUI superBulletCounter;
    public Image multiBulletIcon;
    public TextMeshProUGUI multiBulletCounter;
    public Image knockbackBulletIcon;
    public TextMeshProUGUI knockbackBulletCounter;
    public Image piercingBulletIcon;
    public TextMeshProUGUI piercingBulletCounter;

    [Header("Experience")] public Image experienceBar;
    public TextMeshProUGUI baseLevelExperience;
    public TextMeshProUGUI nextLevelExperience;
    public TextMeshProUGUI currentExperience;

    [Header("Texts")] public TextMeshProUGUI waveText;
    public TextMeshProUGUI levelText;

    [Header("Cooldowns")] public Image speedBuffCooldown;
    public Image fireCooldown;

    [Header("LevelUp Screen")] public GameObject levelUpScreen;

    private PlayerEntity _player;

    #region SingleTon

    public static PlayerUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    private void Start()
    {
        _player = PlayerEntity.Instance;
    }

    private void Update()
    {
        //health
        healthBar.fillAmount = 1.0f * _player.health.hitsLeft / _player.health.playerHits;

        //buffs
        superBulletCounter.text = "X " + _player.buffs.superBulletsLeft;
        superBulletIcon.color =
            _player.buffs.superBulletsLeft == 0
                ? Color.gray
                : Color.white;
        multiBulletCounter.text = "X " + _player.buffs.bulletsMultipliersLeft;
        multiBulletIcon.color =
            _player.buffs.bulletsMultipliersLeft == 0
                ? Color.gray
                : Color.white;
        knockbackBulletCounter.text = "X " + _player.buffs.knockbackBulletsLeft;
        knockbackBulletIcon.color =
            _player.buffs.knockbackBulletsLeft == 0
                ? Color.gray
                : Color.white;
        piercingBulletCounter.text = "X " + _player.buffs.piercingBulletsLeft;
        piercingBulletIcon.color =
            _player.buffs.piercingBulletsLeft == 0
                ? Color.gray
                : Color.white;

        //exp
        experienceBar.fillAmount = 1.0f *
                                   (_player.progression.currentExperience - _player.progression.currentBaseLevelExperience)/
                                   (_player.progression.experienceForNextLevel - _player.progression.currentBaseLevelExperience);
        baseLevelExperience.text = _player.progression.currentBaseLevelExperience.ToString();
        nextLevelExperience.text = _player.progression.experienceForNextLevel.ToString();
        currentExperience.text = _player.progression.currentExperience.ToString();

        //text
        //waveText.text = "Wave " + EnemySpawner.instance.wave;
        levelText.text = "Level " + _player.progression.currentLevel;

        //cooldowns
        speedBuffCooldown.fillAmount = _player.buffs.speedBuffsLeft == 0 || _player.buffs.speedBuffTimeLeft <= 0.0f
            ? 0
            : _player.buffs.speedBuffTimeLeft /
              (_player.buffs.speedBuffTimePerFace * _player.buffs.speedBuffsLeft);
        fireCooldown.fillAmount = 1.0f - (_player.shooting.cooldownLeft / _player.shooting.cooldown);
    }

    public void DisplayLevelUpUI(int statsToLevelUp)
    {
        levelUpScreen.GetComponent<LevelUpScreen>().statsToLevelUp = statsToLevelUp;
        levelUpScreen.SetActive(true);
    }
}