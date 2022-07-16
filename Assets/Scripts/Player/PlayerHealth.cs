using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public LayerMask damagers;
        //public GameObject explosionPrefab;

        private PlayerMovement _playerMovement;
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
            hitsLeft = playerHits;
            _renderer = GetComponent<SpriteRenderer>();
            _defaultMaterial = _renderer.material;
        }

        public void DoDamage(int damage)
        {
            if (_invincible) return;
            if (hitsLeft > 1)
            {
                //AudioManager.Instance.Play("PlayerHit");
                hitsLeft -= damage;
                _renderer.material = hitMaterial;
                _invincible = true;
                Invoke(nameof(RestoreVulnerability), invincibilityFrames * Time.deltaTime);
            }
            else
            {
                //AudioManager.Instance.Play("PlayerDeath");
                Die();
            }
        }

        public void heal(int lifePoints){
            if(hitsLeft < 5)
                hitsLeft = hitsLeft + lifePoints;
        }

        private void RestoreVulnerability()
        {
            _invincible = false;
            _renderer.material = _defaultMaterial;
        }

        private void Die()
        {
            var spawnPos = gameObject.transform.position;
            //Instantiate(explosionPrefab, spawnPos, Quaternion.identity);
            SceneManager.LoadScene(0);
            Destroy(gameObject);
        }
    }
}