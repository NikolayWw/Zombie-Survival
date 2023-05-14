using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Levels
{
    [CreateAssetMenu(fileName = "New LevelDataContainer", menuName = "Static Data/Level Container", order = 0)]
    public class LevelsStaticDataContainer : ScriptableObject
    {
        [field: SerializeField] public List<LevelConfig> LevelsStaticData { get; private set; }

        public void OnValidate()
        {
            foreach (LevelConfig data in LevelsStaticData)
            {
                data.WeaponsSpawnStaticData.ForEach(x => x.OnValidate());
                data.QuestItemsStaticData.ForEach(x => x.OnValidate());
                data.EnemySpawnStaticDataList.ForEach(x => x.OnValidate());
                data.NpcSpawnStaticDataList.ForEach(x => x.OnValidate());
                data.DialogPointStaticDataList.ForEach(x => x.OnValidate());
                data.PropsSpawnStaticDataList.ForEach(x => x.OnValidate());
                data.QuesPointerPositionStaticDataList.ForEach(x => x.OnValidate());
            }
        }
    }
}