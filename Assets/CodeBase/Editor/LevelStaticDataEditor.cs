using CodeBase.Data;
using CodeBase.Logic.Markers;
using CodeBase.Logic.Markers.SpawnMarkers;
using CodeBase.StaticData;
using CodeBase.StaticData.Dialogs;
using CodeBase.StaticData.Enemy;
using CodeBase.StaticData.Items.QuestItems;
using CodeBase.StaticData.Items.WeaponItems;
using CodeBase.StaticData.Levels;
using CodeBase.StaticData.NPC;
using CodeBase.StaticData.Props;
using CodeBase.StaticData.QuestPointer;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Editor
{
    public class LevelStaticDataEditor
    {
        private const string LevelContainerPath = "StaticData/Level/LevelDataContainer";

        [MenuItem("Tools/Collect Level data")]
        private static void Collect()
        {
            var sceneKey = SceneManager.GetActiveScene().name;
            LevelsStaticDataContainer container = Resources.Load<LevelsStaticDataContainer>(LevelContainerPath);
            foreach (LevelConfig data in container.LevelsStaticData)
            {
                if (data.SceneKey == sceneKey)
                {
                    List<WeaponStaticData> weaponStaticDatas = Object.FindObjectsOfType<WeaponSpawnMarker>().Select(x => new WeaponStaticData(x.WeaponDataContainer, x.transform.position)).ToList();
                    List<QuestItemStaticData> questItemStaticDatas = Object.FindObjectsOfType<QuestItemSpawnMarker>().Select(x => new QuestItemStaticData(x.Id, x.Amount, x.transform.position)).ToList();
                    List<EnemySpawnStaticData> enemySpawnStaticDatas = Object.FindObjectsOfType<EnemySpawnMarker>().Select(x => new EnemySpawnStaticData(x.EnemyId, x.transform.position)).ToList();
                    List<NpcSpawnStaticData> ncpSpawnStaticData = Object.FindObjectsOfType<NpcSpawnMarker>().Select(x => new NpcSpawnStaticData(x.NpcId, x.transform.position, x.transform.rotation)).ToList();
                    List<DialogPointStaticData> questPointStaticData = Object.FindObjectsOfType<QuestTriggerPointMarker>().Select(x => new DialogPointStaticData(x.QuestPointId, x.Radius, x.transform.position)).ToList();
                    List<PropsSpawnStaticData> propsStaticData = Object.FindObjectsOfType<PropsSpawnMarker>().Select(x => new PropsSpawnStaticData(x.PropsId, x.transform.position)).ToList();
                    List<QuesPointerPositionStaticData> questPointerPositionData = Object.FindObjectsOfType<QuestPointerPositionWorldMarker>().Select(x => new QuesPointerPositionStaticData(x.PointerId, x.transform.position)).ToList();
                    List<SaveZoneSpawnStaticData> saveZoneSpawnStaticData = Object.FindObjectsOfType<SaveZoneSpawnMarker>().Select(x => new SaveZoneSpawnStaticData(x.transform.position, x.transform.rotation)).ToList();
                    Vector3 playerInitialPosition = GameObject.FindGameObjectWithTag(GameConstants.PlayerInitialPointTag).transform.position;

                    data.SetData(playerInitialPosition,
                        weaponStaticDatas,
                        questItemStaticDatas,
                        enemySpawnStaticDatas,
                        ncpSpawnStaticData,
                        questPointStaticData,
                        propsStaticData,
                        questPointerPositionData,
                        saveZoneSpawnStaticData);

                    container.OnValidate();
                    EditorUtility.SetDirty(container);
                    return;
                }
            }
            Debug.LogError("Seems like the level is missing");
        }
    }
}