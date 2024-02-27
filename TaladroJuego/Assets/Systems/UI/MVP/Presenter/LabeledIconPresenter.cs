using MVPFramework.Model;
using MVPFramework.Presenter;
using System;
using TMPro;
using UISystem.MVP.Model;
using UISystem.MVP.View;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.MVP.Presenter
{
    internal class LabeledIconPresenter : MonoBehaviour,
        IPresenter<LabeledIconPresenter.LabeledIcon, IModel<(Sprite icon, string label)>>,
        IPresenter<LabeledIconPresenter.LabeledIcon, IModel<VisualDescriptibleModel.Data>>
    {
        [Serializable]
        public struct LabeledIcon
        {
            [field: SerializeField]
            public GameObject Root { get; private set; }

            [field: SerializeField]
            public Image Icon { get; private set; }

            [field: SerializeField]
            public TMP_Text Label { get; private set; }

            public LabeledIcon(GameObject root, Image icon, TMP_Text label)
            {
                Root = root;
                Icon = icon;
                Label = label;
            }
        }

        public bool TryPresentElementWith(LabeledIcon element, IModel<(Sprite icon, string label)> model)
        {
            return new IconLabelView(
                new IconView(element.Icon),
                new TextView(element.Label))
                .TryUpdateWith(model.Capture());
        }

        public bool TryPresentElementWith(LabeledIcon element, IModel<VisualDescriptibleModel.Data> model)
        {
            (Sprite icon, string label, _) = model.Capture();
            return new IconLabelView(
                new IconView(element.Icon),
                new TextView(element.Label))
                .TryUpdateWith((icon, label));
        }
    }
}