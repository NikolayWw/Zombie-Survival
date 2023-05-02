using UnityEngine;

namespace CodeBase.Player.Move
{
    public class MoveState : MonoBehaviour
    {
        [field: SerializeField] public MoveStateId Id { get; private set; }
    }
}