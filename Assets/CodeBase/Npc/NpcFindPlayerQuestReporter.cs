using CodeBase.Services.StaticData;
using CodeBase.StaticData.Dialogs;
using CodeBase.StaticData.NPC;
using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Npc
{
    public class NpcFindPlayerQuestReporter : MonoBehaviour
    {
        private readonly Collider[] _playerColliders = new Collider[5];
        private NpcFindPlayerConfig _findPlayerConfig;
        private DialogPointStaticData _dialogPoint;

        public Action OnPlayerTriggered;

        public void Construct(IStaticDataService dataService, NpcId npcId)
        {
            _findPlayerConfig = dataService.NpcFindPlayerConfig;
            _dialogPoint = dataService.ForNpcDialogPoint(npcId);
        }

        private void Start()
        {
            if (_dialogPoint != null)
                StartCoroutine(FindPlayer());
        }

        private IEnumerator FindPlayer()
        {
            var wait = new WaitForSeconds(_findPlayerConfig.FindDelay);
            while (true)
            {
                int count = Physics.OverlapSphereNonAlloc(_dialogPoint.Position, _dialogPoint.SphereRadius, _playerColliders, _findPlayerConfig.PlayerLayer, QueryTriggerInteraction.Ignore);
                if (count > 0)
                {
                    OnPlayerTriggered?.Invoke();
                }

                yield return wait;
            }
        }
    }
}