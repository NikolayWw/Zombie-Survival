using UnityEngine;

namespace CodeBase.StaticData.Inventory
{
    [CreateAssetMenu(fileName = "New AidKitConfig", menuName = "Static Data/Items/Aid Kit", order = 0)]
    public class AidKitConfig : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public int BuyAmount { get; private set; }
        [field: SerializeField] public float HealCount { get; private set; }
    }
}