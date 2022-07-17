using Player;

namespace Pickups
{
    public class FullHealPickup : Pickup
    {
        public override void Effect()
        {
            PlayerEntity.Instance.health.Heal(10);
            //SoundManager.Instance.PlayOneShot("Heal");
        }
    }
}
