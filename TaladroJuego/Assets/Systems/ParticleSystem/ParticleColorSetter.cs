using UnityEngine;

namespace Particles
{
    [RequireComponent(typeof(ParticleSystem))]
    internal class ParticleColorSetter : MonoBehaviour
    {
        private ParticleSystem ps;
        private ParticleSystem.ColorOverLifetimeModule col;
        private Gradient grad;

        private Color startColor = new Color();
        private Color endColor = new Color();

        private void Awake() {
            ps = GetComponent<ParticleSystem>();
            col = ps.colorOverLifetime;
            col.enabled = true;

            grad = new Gradient();
        }

        public void ChangeStartColor(Color color) {
            startColor = color;
            SetGradientKeys();        }
        public void ChangeEndColor(Color color) {
            endColor = color;
            SetGradientKeys();
        }

        private void SetGradientKeys() {
            grad.SetKeys(
                new GradientColorKey[] { 
                new GradientColorKey(startColor, 0.0f), 
                new GradientColorKey(endColor, 1.0f) }, 

                new GradientAlphaKey[] { 
                new GradientAlphaKey(1.0f, 0.0f), 
                new GradientAlphaKey(0.0f, 1.0f) }
                );

            col.color = grad;
        }
    }
}