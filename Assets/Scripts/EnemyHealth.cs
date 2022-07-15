using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth;
    public int CurrentHealth { get; private set; }

    public GameObject[] dropTable;


    private int lastHitId = -1;
    private static int killCounter = 0;
    public static int maxKillCount = 3;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = maxHealth;
    }

    private void Update()
    {

    }

    public bool Damage(int damage, int attackId)
    {
        if (attackId != lastHitId)
        {
            SoundManager.Instance.PlayOneShot("DealDamage");
            lastHitId = attackId;
            CurrentHealth -= damage;
            GetComponent<EnemyMovement>().TakeKnockback();
            if (CurrentHealth <= 0)
            {
                //Destroying Enemy
                Destroy(this.gameObject);
                killCounter++;
                if (Random.Range(0, maxKillCount) < killCounter)
                {
                    int drop = Random.Range(0, dropTable.Length);
                    if (dropTable.Length != 0)
                        Instantiate(dropTable[drop], transform.position, transform.rotation);
                    killCounter = 0;
                }
            }
            return true;
        }
        return false;
    }
}
