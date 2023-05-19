using CodeBase.Logic;
using CodeBase.Logic.Inventory.WeaponInventory;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Items.WeaponItems;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Factory;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Weapon
{
    public class WeaponInteraction : MonoBehaviour
    {
        [SerializeField] private InteractionReporter _interactionReporter;

        private HoverAtMessage _hoverAtMessage;
        private WeaponPieceData _pieceData;
        private WeaponDataContainer _currentWeaponData;
        private IUIFactory _uiFactory;

        private bool _destroyed;
        private IPersistentProgressService _persistentProgressService;
        private WeaponSlotsHandler _slotsHandler;

        public void Construct(WeaponPieceData pieceData, ILogicFactory logicFactory, IUIFactory uiFactory, IPersistentProgressService persistentProgressService)
        {
            _pieceData = pieceData;
            _slotsHandler = logicFactory.WeaponSlotsHandler;
            _currentWeaponData = pieceData.WeaponDataContainer;
            _uiFactory = uiFactory;
            _persistentProgressService = persistentProgressService;

            _interactionReporter.OnSelect += Select;
            _interactionReporter.OnDeselect += Deselect;
            _interactionReporter.OnUse += Use;
        }

        private void OnDestroy()
        {
            CleanWindow();
        }

        private void Select()
        {
            if (CurrentWeaponMissing())
                return;

            if (HoverMessagePresent() == false)
                _hoverAtMessage = _uiFactory.CreateHoverAtMessage(transform.position, transform, transform);

            ShowContext();
        }

        private void Use()
        {
            if (CurrentWeaponMissing())
                return;

            if (_slotsHandler.IsInventoryFull())
                return;

            _slotsHandler.Add(_currentWeaponData);
            DestroyThis();
        }

        private void Deselect()
        {
            CleanWindow();
        }

        private bool CurrentWeaponMissing()
        {
            return _destroyed || _currentWeaponData?.GetData()?.WeaponId == WeaponId.None;
        }

        private void CleanWindow()
        {
            if (HoverMessagePresent())
            {
                _hoverAtMessage.Close();
                _hoverAtMessage = null;
            }
        }

        private void ShowContext()
        {
            if (_slotsHandler.CanAdd(_currentWeaponData.GetData().WeaponId))
                _hoverAtMessage.ShowAddItem(_currentWeaponData.GetData().WeaponId);
            else
                _hoverAtMessage.ShowWeaponInventoryFull();
        }

        private bool HoverMessagePresent() =>
            _hoverAtMessage != null && _hoverAtMessage.Destroyed == false;

        private void DestroyThis()
        {
            Deselect();

            List<WeaponPieceData> weaponPiece = _persistentProgressService.PlayerProgress.WorldData.WeaponPieceDataList;
            if (weaponPiece.Contains(_pieceData))
                weaponPiece.Remove(_pieceData);

            _destroyed = true;
            Destroy(gameObject);
        }
    }
}