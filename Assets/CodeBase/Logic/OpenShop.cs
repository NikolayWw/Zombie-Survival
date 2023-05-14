using CodeBase.Services.StaticData;
using CodeBase.StaticData.NPC;
using CodeBase.UI.Services.Window;
using CodeBase.UI.Windows.Shop;
using UnityEngine;

namespace CodeBase.Logic
{
    public class OpenShop : MonoBehaviour
    {
        [SerializeField] private InteractionReporter _reporter;
        private IWindowService _windowService;
        private IStaticDataService _dataService;
        private NpcId _npcId;

        public void Construct(IWindowService windowService, IStaticDataService dataService, NpcId npcId)
        {
            _windowService = windowService;
            _dataService = dataService;
            _npcId = npcId;
            _reporter.OnUse += Open;
        }

        private void Open()
        {
            _windowService.Open(WindowId.ShopWindow);
            if (_windowService.GetWindow(WindowId.ShopWindow, out ShopWindow shopWindow))
            {
                shopWindow.Initialize(_dataService.ForNpc(_npcId).ShopConfigs);
            }
        }
    }
}