using Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bullets;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth;
    public int CurrentHealth { get; private set; }
    public LayerMask bulletMask;

    public GameObject[] dropTable;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = maxHealth;
    }

    private void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col){
        if (bulletMask.HasLayer(col.gameObject.layer)){
            DiceBullet bullet = col.gameObject.GetComponent<DiceBullet>();
            CurrentHealth -= bullet.GetDamage();
            GetComponent<EnemyMovement>().TakeKnockback();
            if (CurrentHealth <= 0)
            {
                //Destroying Enemy
                Destroy(this.gameObject);
                int drop = Random.Range(-1, dropTable.Length);
                if (dropTable.Length != 0 && drop != -1)
                    Instantiate(dropTable[drop], transform.position, transform.rotation);
                }
            }
        }

    /*public bool Damage(int damage, int attackId)
    {
        if (attackId != lastHitId)
        {
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
    */
}
