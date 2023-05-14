using CodeBase.StaticData.Items.WeaponItems;

namespace CodeBase.Data.WeaponInventory
{
    public static class WeaponDataExtension
    {
        private const string FirearmId = "Firearms_";
        private const string MeleeId = "Melee_";
        private const string GravityGunId = "GravityGun_";

        public static bool IsFirearmsWeapon(WeaponId id) =>
            id.ToString().Contains(FirearmId);

        public static bool IsMeleeWeapon(WeaponId id) =>
             id.ToString().Contains(MeleeId);

        public static bool IsGravityGun(WeaponId id) =>
            id.ToString().Contains(GravityGunId);
    }
}