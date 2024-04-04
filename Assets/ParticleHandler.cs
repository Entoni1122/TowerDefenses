using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class ParticleHandler : MonoBehaviour
    {
        private void OnParticleSystemStopped()
        {
            PoolingMethod.ReturnObjectToPool(this.gameObject);
        }
    }
}
