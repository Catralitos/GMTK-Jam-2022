using UnityEngine;

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

    public int maxPopulation;
    int population;

    void Update()
    {
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
        return population >= maxPopulation;
    }

    public void WarnDeath() {
        population--;
    }


    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Camera.main.transform.position, spawnOffset);
    }
}
