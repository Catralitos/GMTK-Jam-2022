using UnityEngine;

public class StatsCollector : MonoBehaviour {
    [System.Serializable]
    public struct PlayerStats {
        public int smallEnemiesKilled;
        public int largeEnemiesKilled;
        public int baseEnemiesKilled;
        public int totalExpObtained;
        public int wavesCleared;
        public float timeSurvived;
        public int superBulletsShot;
        public int knockbackBulletsShot;
        public int multiBulletsShot;
        public int piercingBulletsShot;
    }

    static StatsCollector instance;
    PlayerStats stats;

    private void Awake() {
        instance = this;

    }

    public static void KillSmall() {
        if(instance == null) return;
        instance.stats.smallEnemiesKilled++;
    }
    public static void KillBase() {
        if(instance == null) return;
        instance.stats.baseEnemiesKilled++;

    }
    public static void KillLarge() {
        if(instance == null) return;
        instance.stats.largeEnemiesKilled++;
    }

    public static void ClearedWave() {
        if(instance == null) return;
        instance.stats.wavesCleared++;
    }

    public static void ObtainedExp(int value) {
        if(instance == null) return;
        instance.stats.totalExpObtained += value;
    }

    public static void SurvivedTime(float delta) {
        if(instance == null) return;
        instance.stats.timeSurvived += delta;
    }

    public static void ShotPiercing() {
        if(instance == null) return;
        instance.stats.piercingBulletsShot++;
    }

    public static void ShotMultiple() {
        if(instance == null) return;
        instance.stats.multiBulletsShot++;
    }

    public static void ShotSuper() {
        if(instance == null) return;
        instance.stats.superBulletsShot++;
    }

    public static void ShotKnockback() {
        if(instance == null) return;
        instance.stats.knockbackBulletsShot++;
    }

    public static PlayerStats GetStats() {
        if(instance == null) return new PlayerStats();
        return instance.stats;
    }
}