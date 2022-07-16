using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    private void Awake() {
        if(instance) {
            return;
        }
        instance = this;
    }

    [System.Serializable]
    public struct SpawnableEnemy { 
        public GameObject type;
        public int spawnRate;
    }

    public SpawnableEnemy[] enemies;

    [Tooltip("How far away from the player enemies should spawn")]
    public float spawnOffset;

    public float cooldown;
    float onCooldownTime;

    float timeSinceWaveStart = 0;

    [Header("Population Curve Parameters")]
    public int hardCap;
    public int baseValue;
    public float growthFactor;
    public float waveTime = 15;
    public float waveCooldown = 5;
    float waveCooldownTime;
    int waveBasePop;
    int currentMaxPop;
    [HideInInspector]
    public int wave = 1;
    int population;

    bool onWaveCooldown;

    [Header("Debug")]
    public TMP_Text text;

    void Start() {
        waveBasePop = baseValue;   
        currentMaxPop = waveBasePop;
    }

    void Update()
    {
        if(!onWaveCooldown)
            timeSinceWaveStart += Time.deltaTime;
        if(timeSinceWaveStart > waveTime && !onWaveCooldown) {
            BeginWaveCooldown();
        }
        if(onWaveCooldown && waveCooldownTime >= waveCooldown) {
            ChangeWave();
        }
        waveCooldownTime += Time.deltaTime;
        SetDebugUI();
        if(onCooldownTime <= 0) {
            if(OverPopulated()) return;
            DoSpawnCycle();
        }
        else {
            onCooldownTime -= Time.deltaTime;
        }
    }

    void DoSpawnCycle() {
        Spawn(PickPosition(), PickEnemy());
        ResetTimer();
    }

    Vector3 PickPosition() {
        float angle = Random.Range(0, 2*Mathf.PI);
        Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
        pos *= spawnOffset;
        pos += Camera.main.transform.position;
        pos.z = 0;
        return pos;
    }

    SpawnableEnemy PickEnemy() {
        int maxRoll = 0;
        foreach (SpawnableEnemy se in enemies)
        {
            maxRoll += se.spawnRate;
        }
        int roll = Random.Range(0, maxRoll);
        foreach (SpawnableEnemy se in enemies)
        {
            roll -= se.spawnRate;
            if(roll <= 0) {
                return se;
            }
        }
        Debug.LogWarning("Rolled Higher than Possible");
        return enemies[0];
    }

    void Spawn(Vector3 position, SpawnableEnemy enemy) {
        Instantiate(enemy.type, position, Quaternion.identity);
        population++;

    }

    void ResetTimer() {
        onCooldownTime = cooldown;
    }

    bool OverPopulated() {
        return population >= currentMaxPop || population > hardCap;
    }

    public void WarnDeath() {
        population--;
    }

    void ChangeWave() {
        onWaveCooldown = false;
        timeSinceWaveStart = 0;
        wave++;
        waveBasePop = Mathf.FloorToInt(baseValue + growthFactor*(wave - 1));
        currentMaxPop = waveBasePop;
    }

    void BeginWaveCooldown() {
        onWaveCooldown = true;
        waveCooldownTime = 0;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Camera.main.transform.position, spawnOffset);
    }

    void SetDebugUI() {
        if(text == null) return;
        text.text = "Wave: " + wave + "\nTime: " + timeSinceWaveStart.ToString("00.00") + "\nMax Pop: " + currentMaxPop;
    }
}
