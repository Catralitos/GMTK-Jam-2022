using UnityEngine;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        public enum State
        {
            Pathfinding,
            Knockback,
        }
        public float moveSpeed = 3f;
        public float knockbackSpeed = 10f;
        public float knockbackDuration = 0.125f;
        public float turningSpeed = 0.5f;

        private GameObject target;
        private Rigidbody2D rb;
        private Vector2 knockbackDirection;
        private float knockbackStartTime;
        private Vector2 currentDirection;
        private State state;
        private Animator animator;


        private void Start()
        {
            if(Player.PlayerEntity.Instance != null)
                target = Player.PlayerEntity.Instance.gameObject;
            rb = GetComponent<Rigidbody2D>();
            state = State.Pathfinding;
            currentDirection = GetVectorNormalizedToTarget();
            animator = GetComponent<Animator>();
        }

    

        void FixedUpdate()
        {
            switch (state)
            {
                case State.Pathfinding:
                    CalculateNewDirection();
                    animator.SetFloat("Horizontal", currentDirection.x);
                    animator.SetFloat("Vertical", currentDirection.y);
                    animator.SetFloat("Speed", 1);

                    float skillMod = 1.0f;
                    if(PlayerSkills.instance.IsUnlocked(PlayerSkills.Upgrades.SlowerEnemies)) skillMod = PlayerSkills.instance.slowFactorOnUpgarde;

                    rb.MovePosition(rb.position + currentDirection * moveSpeed * Time.fixedDeltaTime * skillMod);
                    break;
                case State.Knockback:
                    rb.MovePosition(rb.position + knockbackDirection * knockbackSpeed * Time.fixedDeltaTime);
                    float knockbackActualDuration = knockbackDuration;
                    if(PlayerSkills.instance.IsUnlocked(PlayerSkills.Upgrades.Knockback)) knockbackActualDuration *= PlayerSkills.instance.knockbackFactorOnUpgarde;

                    if (Time.time - knockbackStartTime > knockbackActualDuration)
                        state = State.Pathfinding;
                    break;
            }
        }

        public void CalculateNewDirection(){
            /*
        Vector2 DirToTarget = GetVectorNormalizedToTarget();
        Vector2 DirAdjust = DirToTarget - currentDirection;
        DirAdjust.Normalize();
        currentDirection += DirAdjust*turningSpeed;
        currentDirection.Normalize();
        */

            currentDirection = GetVectorNormalizedToTarget();
        }

        public Vector2 GetVectorNormalizedToTarget(){
            if(Player.PlayerEntity.Instance == null)
                return (Vector2)currentDirection;
            Vector2 VectorToTarget;
            VectorToTarget = (Vector2)target.transform.position - rb.position;
            VectorToTarget.Normalize();
            return VectorToTarget;
        }

        public void TakeKnockback(bool isShockwave)
        {
            if(Player.PlayerEntity.Instance == null)
                return;
            knockbackStartTime = Time.time;
            knockbackDirection = isShockwave ? transform.position - Player.PlayerEntity.Instance.gameObject.transform.position : transform.position - target.transform.position;
            state = State.Knockback;
        }
    }
}
