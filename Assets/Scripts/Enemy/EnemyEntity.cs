using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyEntity : MonoBehaviour
{
    public EnemyHealth Health { get; private set; }

    public enum EnemyType{
        baseEnemy,
        smallEnemy,
        largeEnemy
    }

    public EnemyType type;

    void OnEnable()
    {
        Health = GetComponent<EnemyHealth>();
    }

    private void OnDestroy() {
        if(type == EnemyType.baseEnemy)
            StatsCollector.KillBase();
        else if(type == EnemyType.smallEnemy)
            StatsCollector.KillSmall();
        else if(type == EnemyType.largeEnemy)
            StatsCollector.KillLarge();
        EnemySpawner.instance.WarnDeath();
    }
}
