using MVPFramework.View;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.MVP.View
{
    internal readonly struct IconView : IView<Sprite>
    {
        private readonly Image _image;

        public IconView(Image image)
        {
            _image = image;
        }

        public bool TryUpdateWith(Sprite sprite) =>
            _image.sprite = sprite;
    }
}