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
            int expAux = exp;
            //Destroying Enemy
            Destroy(this.gameObject);
            //while(expAux > 0){
            Instantiate(expPickup, transform.position, transform.rotation);
            //expAux = expAux/10; 
            }
            //PlayerEntity.Instance.progression.AddExperience(exp);
            int drop = Random.Range(-1, dropTable.Length);
            if (dropTable.Length != 0 && drop != -1)
                Instantiate(dropTable[drop], transform.position, transform.rotation);
        }
    }
        
}
