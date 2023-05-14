using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Inventory.WeaponInventory.Logic
{
    public class DropWeaponWindow : BaseWindow
    {
        [SerializeField] private Image _backgroundImage;

        public void UpdateImage(float value)
        {
            _backgroundImage.fillAmount = value;
        }
    }
}