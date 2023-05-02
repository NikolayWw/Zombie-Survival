using CodeBase.StaticData.Minimap;
using CodeBase.StaticData.Player.Move;
using UnityEngine;

namespace CodeBase.StaticData.Player
{
    [CreateAssetMenu(fileName = "New PlayerConfig", menuName = "Static Data/Player", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public MoveStaticData MoveStaticData { get; private set; }
        [field: SerializeField] public float MaxHealth { get; private set; } = 100f;
        [field: SerializeField] public int Money { get; private set; } = 10_000;
        [field: SerializeField] public int AidKitAmount { get; private set; } = 5;
        [field: SerializeField] public float InteractionDelaySelect { get; private set; } = 0.1f;
        [field: SerializeField] public float InteractionSelectRayDistance { get; private set; } = 55f;
        [field: SerializeField] public LayerMask InteractionLayerMask { get; private set; }
        [field: SerializeField] public MinimapWorldIconConfig MinimapWorldIconConfig { get; private set; }

        private void OnValidate()
        {
            MoveStaticData.SwitchMoveConfig.OnValidate();
        }
    }
}