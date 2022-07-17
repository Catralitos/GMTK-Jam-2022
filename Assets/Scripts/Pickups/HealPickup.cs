using Player;

namespace Pickups
{
    public class HealPickup : Pickup
    {
        public override void Effect()
        {
            PlayerEntity.Instance.health.Heal(1);
            //SoundManager.Instance.PlayOneShot("Heal");
        }
    }
}
