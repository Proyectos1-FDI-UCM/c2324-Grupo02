using MVPFramework.View;
using System;
using UnityEngine;
using static UISystem.MVP.View.IconLabelView;

namespace UISystem.MVP.View
{
    internal readonly struct IconLabelView : IView<LabeledSprite>, IView<Sprite>, IView<string>
    {
        [Serializable]
        public struct LabeledSprite
        {
            [field: SerializeField]
            public Sprite Icon { get; private set; }

            [field: SerializeField]
            public string Label { get; private set; }

            public LabeledSprite(Sprite icon, string label)
            {
                Icon = icon;
                Label = label;
            }
        }

        private readonly IView<Sprite> _iconView;
        private readonly IView<string> _labelView;

        public IconLabelView(IView<Sprite> iconView, IView<string> labelView)
        {
            _iconView = iconView;
            _labelView = labelView;
        }

        public bool TryUpdateWith(LabeledSprite status) =>
            _iconView.TryUpdateWith(status.Icon) && _labelView.TryUpdateWith(status.Label);

        public bool TryUpdateWith(Sprite status) => _iconView.TryUpdateWith(status);

        public bool TryUpdateWith(string status) => _labelView.TryUpdateWith(status);
    }
}