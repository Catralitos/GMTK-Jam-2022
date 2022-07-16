using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyEntity : MonoBehaviour
{
    public EnemyHealth Health { get; private set; }

    void OnEnable()
    {
        Health = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        
    }

    private void OnDestroy() {
        EnemySpawner.instance.WarnDeath();
    }
}
