using Pickups;
using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        public static int deathCount;
        [SerializeField] public int maxHealth;
        [SerializeField] public int exp;
        public int CurrentHealth { get; private set; }
        public LayerMask bulletMask;
        public GameObject expPickup;
        public GameObject[] dropTable;
    
        private SpriteRenderer _renderer;
        private Material _defaultMaterial;
        public Material hitMaterial;
        public int whiteFrames;

        private bool _isDead;


        // Start is called before the first frame update
        void Start()
        {
            CurrentHealth = maxHealth;
            _isDead = false;
            _renderer = GetComponent<SpriteRenderer>();
            _defaultMaterial = _renderer.material;
        }

        private void Update()
        {
            if(Vector3.Distance(transform.position, Camera.main.transform.position) > 20) Destroy(this.gameObject);
        }

        public void DoDamage(int damage){
            CurrentHealth -= damage;
            GetComponent<EnemyMovement>().TakeKnockback(false);
            _renderer.material = hitMaterial;
            Invoke(nameof(RestoreColor), whiteFrames * Time.deltaTime);
            if (CurrentHealth <= 0 && _isDead == false)
            {
                _isDead = true;
                if(PlayerSkills.instance.IsUnlocked(PlayerSkills.Upgrades.HealthDrops)) {
                    if(deathCount >= PlayerSkills.instance.numberOfDeathsTillDrop) {
                        //TO DO: Drop full hp pack
                        deathCount += 0;
                    }
                    deathCount++;
                }
                //Destroying Enemy
                GameObject exp_drop = Instantiate(expPickup, transform.position, transform.rotation) as GameObject;;
                exp_drop.GetComponentInChildren<ExpPickup>().Set(exp);
                //PlayerEntity.Instance.progression.AddExperience(exp);
                int drop = Random.Range(-1, dropTable.Length);
                if (dropTable.Length != 0 && drop != -1)
                    if(dropTable[drop].name == "Full heal"){
                        int test = Random.Range(0, 21);
                        Debug.Log(test);
                        if(test == 0)
                            Instantiate(dropTable[drop], transform.position, transform.rotation);
                    }
                    else
                        Instantiate(dropTable[drop], transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }
    
        private void RestoreColor()
        {
            _renderer.material = _defaultMaterial;
        }
        
    }
}
