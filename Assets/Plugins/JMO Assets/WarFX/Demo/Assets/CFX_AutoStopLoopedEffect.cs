using UnityEngine;

// Cartoon FX  - (c) 2015 Jean Moreno
//
// Script handling looped effect in the Demo Scene, so that they eventually stop

namespace Plugins.JMO_Assets.WarFX.Demo.Assets
{
    [RequireComponent(typeof(ParticleSystem))]
    public class CFX_AutoStopLoopedEffect : MonoBehaviour
    {
        public float effectDuration = 2.5f;
        private float d;

        private void OnEnable()
        {
            d = effectDuration;
        }

        private void Update()
        {
            if (d > 0)
            {
                d -= Time.deltaTime;
                if (d <= 0)
                {
                    this.GetComponent<ParticleSystem>().Stop(true);

                    CFX_Demo_Translate translation = this.gameObject.GetComponent<CFX_Demo_Translate>();
                    if (translation != null)
                    {
                        translation.enabled = false;
                    }
                }
            }
        }
    }
}