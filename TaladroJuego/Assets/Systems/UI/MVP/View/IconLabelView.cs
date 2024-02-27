using MVPFramework.View;
using UnityEngine;

namespace UISystem.MVP.View
{
    internal readonly struct IconLabelView : IView<(Sprite icon, string label)>
    {
        private readonly IView<Sprite> _iconView;
        private readonly IView<string> _labelView;

        public IconLabelView(IView<Sprite> iconView, IView<string> labelView)
        {
            _iconView = iconView;
            _labelView = labelView;
        }

        public bool TryUpdateWith((Sprite icon, string label) model) =>
            _iconView.TryUpdateWith(model.icon) && _labelView.TryUpdateWith(model.label);
    }
}