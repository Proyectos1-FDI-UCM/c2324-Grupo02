using MVPFramework.Model;
using UnityEngine;

namespace UISystem.MVP
{
    [CreateAssetMenu(fileName = "Visual Descriptible Model", menuName = "UI/MVP/VisualDescriptibleModel")]
    internal class VisualDescriptibleModel : ScriptableObject,
        IModel<VisualDescriptibleModel.Data>,
        IModel<(string name, string description)>,
        IModel<(Sprite sprite, string name, string description)>
    {
        [SerializeField]
        private Sprite _sprite;

        [SerializeField]
        private DescriptibleModel _descriptible;

        public Data Capture() => new Data(_sprite, _descriptible.Capture());

        (string name, string description) IModel<(string name, string description)>.Capture() =>
            ((IModel<(string name, string description)>)_descriptible).Capture();

        (Sprite sprite, string name, string description) IModel<(Sprite sprite, string name, string description)>.Capture()
        {
            (string name, string description) = ((IModel<(string name, string description)>)_descriptible).Capture();
            return (_sprite, name, description);
        }

        public readonly struct Data
        {
            public readonly string name;
            public readonly string description;
            public readonly Sprite sprite;

            public Data(Sprite sprite, DescriptibleModel.Data data)
            {
                this.sprite = sprite;
                name = data.name;
                description = data.description;
            }

            public Data(Sprite sprite, string name, string description)
            {
                this.sprite = sprite;
                this.name = name;
                this.description = description;
            }

            public void Deconstruct(out Sprite sprite, out string name, out string description)
            {
                sprite = this.sprite;
                name = this.name;
                description = this.description;
            }
        }
    }

}