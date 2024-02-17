using UnityEngine;
using UpgradesSystem.Applicable;

namespace UpgradesSystem.Flyweight.Applicable.Test
{
    [CreateAssetMenu(menuName = "Upgrades/Flyweight/Applicable/Test/TestApplicableUpgradeFlyweigth")]
    internal class TestApplicableUpgradeFlyweigth : ApplicableUpgradeFlyweight
    {
        [SerializeField]
        [TextArea]
        private string _message = "Upgrade applied";
        private readonly struct TestApplicableUpgrade : IApplicableUpgrade
        {
            private readonly string _message;

            public TestApplicableUpgrade(string message)
            {
                _message = message;
            }

            public void Apply()
            {
                Debug.Log(_message);
            }
        }

        public override IApplicableUpgrade Create()
        {
            return new TestApplicableUpgrade(_message);
        }

    }
}