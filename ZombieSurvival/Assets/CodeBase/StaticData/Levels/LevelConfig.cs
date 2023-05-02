using CodeBase.StaticData.Dialogs;
using CodeBase.StaticData.Enemy;
using CodeBase.StaticData.Items.QuestItems;
using CodeBase.StaticData.Items.WeaponItems;
using CodeBase.StaticData.NPC;
using CodeBase.StaticData.Props;
using CodeBase.StaticData.QuestPointer;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Levels
{
    [Serializable]
    public class LevelConfig
    {
        [field: SerializeField] public string SceneKey { get; private set; }
        [field: SerializeField] public Vector3 PlayerInitialPosition { get; private set; }
        [field: SerializeField] public QuestPointerId QuestPointerInitialId { get; private set; }
        [field: SerializeField] public List<WeaponStaticData> WeaponsSpawnStaticData { get; private set; }
        [field: SerializeField] public List<QuestItemStaticData> QuestItemsStaticData { get; private set; }
        [field: SerializeField] public List<EnemySpawnStaticData> EnemySpawnStaticDataList { get; private set; }
        [field: SerializeField] public List<NpcSpawnStaticData> NpcSpawnStaticDataList { get; private set; }
        [field: SerializeField] public List<DialogPointStaticData> DialogPointStaticDataList { get; private set; }
        [field: SerializeField] public List<PropsSpawnStaticData> PropsSpawnStaticDataList { get; private set; }
        [field: SerializeField] public List<QuesPointerPositionStaticData> QuesPointerPositionStaticDataList { get; private set; }
        [field: SerializeField] public List<SaveZoneSpawnStaticData> SaveZoneList { get; private set; }

        public void SetData(Vector3 playerInitialPosition,
            List<WeaponStaticData> weaponStaticData,
            List<QuestItemStaticData> questItemsStaticData,
            List<EnemySpawnStaticData> enemySpawnStaticDataList,
            List<NpcSpawnStaticData> npcSpawnStaticDataList,
            List<DialogPointStaticData> questPointStaticDataList,
            List<PropsSpawnStaticData> propsSpawnStaticDataList,
            List<QuesPointerPositionStaticData> quesPointerPositionStaticDataList,
            List<SaveZoneSpawnStaticData> saveZoneList)
        {
            PlayerInitialPosition = playerInitialPosition;
            WeaponsSpawnStaticData = weaponStaticData;
            QuestItemsStaticData = questItemsStaticData;
            EnemySpawnStaticDataList = enemySpawnStaticDataList;
            NpcSpawnStaticDataList = npcSpawnStaticDataList;
            DialogPointStaticDataList = questPointStaticDataList;
            PropsSpawnStaticDataList = propsSpawnStaticDataList;
            QuesPointerPositionStaticDataList = quesPointerPositionStaticDataList;
            SaveZoneList = saveZoneList;
        }
    }
}