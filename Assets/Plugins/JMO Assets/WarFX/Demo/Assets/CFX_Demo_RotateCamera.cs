using UnityEngine;

namespace Plugins.JMO_Assets.WarFX.Demo.Assets
{
    public class CFX_Demo_RotateCamera : MonoBehaviour
    {
        public static bool rotating = true;

        public float speed = 30.0f;
        public Transform rotationCenter;

        private void Update()
        {
            if (rotating)
                transform.RotateAround(rotationCenter.position, Vector3.up, speed * Time.deltaTime);
        }
    }
}