using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Shop
{
    public class UIShopSlot : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _nameText;

        private Action _onClickDown;

        public void Initialize(string itemName, Sprite icon, Action onClickDown)
        {
            _nameText.text = itemName;
            _iconImage.sprite = icon;
            _onClickDown = onClickDown;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _onClickDown?.Invoke();
        }
    }
}