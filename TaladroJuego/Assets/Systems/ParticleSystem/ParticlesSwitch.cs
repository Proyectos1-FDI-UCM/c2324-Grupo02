using UnityEngine;

namespace Particles
{
    [RequireComponent(typeof(ParticleSystem))]
    internal class ParticlesSwitch : MonoBehaviour
    {
        private ParticleSystem ps;


        private void Awake() {
            ps = GetComponent<ParticleSystem>();
        }
    }
}