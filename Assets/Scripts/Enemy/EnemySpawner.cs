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
    public float waveTime = 30;
    public float waveCooldown = 10;
    float waveCooldownTime;
    int waveBasePop;
    int currentMaxPop;
    int wave = 1;
    int population;

    bool onWaveCooldown;

    [Header("Debug")]
    public TMP_Text text;

    void Start() {
        waveBasePop = baseValue;   
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
        if(!onWaveCooldown)
            SetMaxPopulation();
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
        waveBasePop = currentMaxPop;
    }

    void SetMaxPopulation() {
        //Old curves I disliked. Require differnt values for growthFactor
        //float v = Mathf.Floor(Mathf.Log10(timeSinceWaveStart + 1));
        //v = Mathf.Floor(100/((-0.75f * timeSinceWaveStart) + 35) - 2.85f);
        float v = timeSinceWaveStart*timeSinceWaveStart;
        currentMaxPop = (int)Mathf.Floor(waveBasePop + v*growthFactor);
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
        text.text = "Wave: " + wave + "\nTime: " + timeSinceWaveStart.ToString("00.00") + "\nMax Pop: " + currentMaxPop;
    }
}
