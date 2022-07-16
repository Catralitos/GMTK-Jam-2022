using Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bullets;
using Player;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth;
    [SerializeField] public int exp;
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
        if(Vector3.Distance(transform.position, Camera.main.transform.position) > 20) Destroy(this.gameObject);
    }

    public void DoDamage(int damage){
        CurrentHealth -= damage;
        GetComponent<EnemyMovement>().TakeKnockback();
        if (CurrentHealth <= 0)
        {
            //Destroying Enemy
            Destroy(this.gameObject);
            PlayerEntity.Instance.progression.AddExperience(exp);
            int drop = Random.Range(-1, dropTable.Length);
            if (dropTable.Length != 0 && drop != -1)
                Instantiate(dropTable[drop], transform.position, transform.rotation);
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
