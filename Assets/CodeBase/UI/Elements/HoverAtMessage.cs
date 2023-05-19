using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items.QuestItems;
using CodeBase.StaticData.Items.WeaponItems;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class HoverAtMessage : MonoBehaviour
    {
        [SerializeField] private TMP_Text _messageText;
        private IStaticDataService _dataService;
        public bool Destroyed { get; private set; }

        public void Construct(IStaticDataService dataService)
        {
            _dataService = dataService;
        }

        public void ShowAddItem(WeaponId weaponId)
        {
            CleanAllText();
            var config = _dataService.ForWeapon(weaponId);
            _messageText.text = $"Add: {config.Name}";
        }

        public void ShowAddItem(QuestItemId questItemId)
        {
            CleanAllText();
            var config = _dataService.ForQuestItem(questItemId);
            _messageText.text = $"Add: {config.Name}";
        }

        public void ShowWeaponInventoryFull()
        {
            _messageText.text = $"Inventory full";
        }

        public void Close()
        {
            Destroyed = true;
            Destroy(gameObject);
        }

        private void CleanAllText()
        {
            _messageText.text = string.Empty;
        }
    }
}