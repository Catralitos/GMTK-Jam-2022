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
    public GameObject expPickup;
    public GameObject[] dropTable;
    private bool isDead;


    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = maxHealth;
        isDead = false;
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, Camera.main.transform.position) > 20) Destroy(this.gameObject);
    }

    public void DoDamage(int damage){
        CurrentHealth -= damage;
        GetComponent<EnemyMovement>().TakeKnockback(false);
        if (CurrentHealth <= 0 && isDead == false)
        {
            isDead = true;
            //Destroying Enemy
            GameObject exp_drop = Instantiate(expPickup, transform.position, transform.rotation) as GameObject;;
            exp_drop.GetComponentInChildren<ExpPickup>().set(exp);
            //PlayerEntity.Instance.progression.AddExperience(exp);
            int drop = Random.Range(-1, dropTable.Length);
            if (dropTable.Length != 0 && drop != -1)
                Instantiate(dropTable[drop], transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
        
}
