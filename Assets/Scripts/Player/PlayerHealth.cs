using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public LayerMask damagers;
        //public GameObject explosionPrefab;

        private PlayerMovement _playerMovement;
        private PlayerShooting _playerShooting;
        private SpriteRenderer _renderer;
        private Material _defaultMaterial;
        public Material hitMaterial;
        public int playerHits = 5;
        public int hitsLeft = 5;
        public int invincibilityFrames;
        private bool _invincible;

        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerShooting = GetComponent<PlayerShooting>();
            hitsLeft = playerHits;
            _renderer = GetComponent<SpriteRenderer>();
            _defaultMaterial = _renderer.material;
        }

        public void DoDamage(int damage)
        {
            if (_invincible) return;
            GameManager.Instance.audioManager.Play("takingdamage");
            if (hitsLeft > 1)
            {
                //AudioManager.Instance.Play("PlayerHit");
                if(PlayerSkills.instance.IsUnlocked(PlayerSkills.Upgrades.DamageReduction)) damage = Mathf.FloorToInt(damage * PlayerSkills.instance.reductionFactorOnReductionUpgrade);
                hitsLeft -= damage;
                _renderer.material = hitMaterial;
                _invincible = true;
                Invoke(nameof(RestoreVulnerability), invincibilityFrames * Time.deltaTime);
                _playerShooting.doShockwave();
            }
            else
            {
                //AudioManager.Instance.Play("PlayerDeath");
                Die();
            }
        }

        public void Heal(int lifePoints){
            if(hitsLeft < 5)
                hitsLeft += lifePoints;
            else
                hitsLeft = playerHits;
        }

        public void FullyHeal() {
            hitsLeft = playerHits;
        }

        private void RestoreVulnerability()
        {
            _invincible = false;
            _renderer.material = _defaultMaterial;
        }

        private void Die()
        {
            //var spawnPos = gameObject.transform.position;
            //Instantiate(explosionPrefab, spawnPos, Quaternion.identity);
            SceneManager.LoadScene(2);
            Destroy(gameObject);
        }
    }
}