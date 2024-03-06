using MVPFramework.Model;
using UnityEngine;
using static UISystem.MVP.Model.DescriptibleModel;

namespace UISystem.MVP.Model
{
    [CreateAssetMenu(fileName = "Descriptible Model", menuName = "UI/MVP/DescriptibleModel")]
    internal class DescriptibleModel : ScriptableObject, IModel<TitledDescription>
    {
        [SerializeField]
        private string _name;

        [SerializeField]
        [TextArea]
        private string _description;

        public TitledDescription Capture() => new TitledDescription(_name, _description);

        public readonly struct TitledDescription
        {
            public readonly string title;
            public readonly string description;

            public TitledDescription(string title, string description)
            {
                this.title = title;
                this.description = description;
            }

            public void Deconstruct(out string title, out string description)
            {
                title = this.title;
                description = this.description;
            }
        }
    }
}