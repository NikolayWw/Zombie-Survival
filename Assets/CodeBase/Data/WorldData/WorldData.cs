using CodeBase.Data.Props;
using CodeBase.StaticData.Items.WeaponItems;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Data.WorldData
{
    [Serializable]
    public class WorldData
    {
        [field: SerializeField] public string SceneKey { get; private set; }

        [field: SerializeField] public List<WeaponPieceData> WeaponPieceDataList { get; private set; }
        [field: SerializeField] public List<EnemyPieceData> EnemyDataPieceList { get; private set; }
        [field: SerializeField] public List<NpcPieceData> NpcDataPieceList { get; private set; }
        [field: SerializeField] public List<QuestItemPieceData> QuestItemPieceDataList { get; private set; }
        [field: SerializeField] public List<PropsPieceData> EnvironmentDataList { get; private set; }
        [field: SerializeField] public QuestPointerPieceData QuestPointerPieceData { get; private set; }

        public WorldData(string sceneKey, List<WeaponPieceData> weaponPieceDataList, List<EnemyPieceData> enemyDataPieceList, List<NpcPieceData> npcDataPieceList, List<QuestItemPieceData> questItemPieceDataList, List<PropsPieceData> environmentDataList, QuestPointerPieceData questPointerPieceData)
        {
            SceneKey = sceneKey;
            WeaponPieceDataList = weaponPieceDataList;
            EnemyDataPieceList = enemyDataPieceList;
            NpcDataPieceList = npcDataPieceList;
            QuestItemPieceDataList = questItemPieceDataList;
            EnvironmentDataList = environmentDataList;
            QuestPointerPieceData = questPointerPieceData;
        }

        public void SetSceneKey(string sceneKey) =>
            SceneKey = sceneKey;
    }
}