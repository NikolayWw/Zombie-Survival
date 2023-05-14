using UnityEngine;

namespace CodeBase.UI.Windows.QuestPointer
{
    public class QuestUIPointerTarget : MonoBehaviour
    {
        [SerializeField] private GameObject _outScreeObj;
        [SerializeField] private GameObject _inScreeObj;
        private bool _closed;

        public void SetPositionAndRotate(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
        }

        public void Close()
        {
            if (_closed)
                return;

            _closed = true;
            Destroy(gameObject);
        }

        public void ShowOutScreen()
        {
            _outScreeObj.SetActive(true);
            _inScreeObj.SetActive(false);
        }

        public void ShowInScreen()
        {
            _outScreeObj.SetActive(false);
            _inScreeObj.SetActive(true);
        }
    }
}