using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items.QuestItems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Inventory.QuestInventory
{
    public class UIQuestItemSlotUpdate : MonoBehaviour
    {
        [SerializeField] private Image _imageIcon;
        [SerializeField] private TMP_Text _amountText;

        private IStaticDataService _dataService;
        public QuestItemId Questsad;

        public void Construct(IStaticDataService dataService)
        {
            _dataService = dataService;
        }

        public void Refresh(QuestItemId id, int amount)
        {
            Questsad = id;
            QuestItemConfig config = _dataService.ForQuestItem(id);
            _imageIcon.sprite = config.IconSprite;
            _amountText.text = config.CanStack ? amount.ToString() : string.Empty;
        }

        public void Remove()
        {
            Destroy(gameObject);
        }
    }
}