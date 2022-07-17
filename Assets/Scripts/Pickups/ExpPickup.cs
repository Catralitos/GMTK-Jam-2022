using Player;

namespace Pickups
{
    public class ExpPickup : Pickup
    {
        private int exp;

        public void Set(int newExp){
            exp = newExp;
            transform.localScale *= exp/50.0f;
        }

        public override void Effect()
        {
            PlayerEntity.Instance.progression.AddExperience(exp);
            //SoundManager.Instance.PlayOneShot("Exp");
        }
    }
}
