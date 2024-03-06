using MVPFramework.Model;
using UnityEngine;
using static UISystem.MVP.Model.Data.Descriptible;
using static UISystem.MVP.Model.Data.VisualDescriptible;

namespace UISystem.MVP.Model.Data
{
    [CreateAssetMenu(fileName = "Visual Descriptible Model", menuName = "UI/MVP/VisualDescriptibleModel")]
    internal class VisualDescriptible : ScriptableObject, IModel<PortrayedDescription>,
        IModel<TitledDescription>
    {
        [SerializeField]
        private Sprite _sprite;

        [SerializeField]
        private Descriptible _descriptible;

        public PortrayedDescription Capture() => new PortrayedDescription(_descriptible.Capture(), _sprite);
        TitledDescription IModel<TitledDescription>.Capture() => _descriptible.Capture();

        public readonly struct PortrayedDescription
        {
            public readonly string title;
            public readonly string description;
            public readonly Sprite sprite;

            public PortrayedDescription(TitledDescription data, Sprite sprite)
            {
                title = data.title;
                description = data.description;
                this.sprite = sprite;
            }

            public PortrayedDescription(string title, string description, Sprite sprite)
            {
                this.title = title;
                this.description = description;
                this.sprite = sprite;
            }

            public void Deconstruct(out string title, out string description, out Sprite sprite)
            {
                title = this.title;
                description = this.description;
                sprite = this.sprite;
            }
        }
    }

}