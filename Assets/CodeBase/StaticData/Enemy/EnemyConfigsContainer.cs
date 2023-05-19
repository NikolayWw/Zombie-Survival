using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Enemy
{
    [CreateAssetMenu(fileName = "New EnemyConfigsContainer", menuName = "Static Data/Enemy Config Container", order = 0)]
    public class EnemyConfigsContainer : ScriptableObject
    {
        [field: SerializeField] public List<EnemyConfig> EnemyConfigs { get; private set; }
    }
}