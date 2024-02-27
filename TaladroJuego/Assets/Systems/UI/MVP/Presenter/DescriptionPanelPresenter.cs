using MVPFramework.Model;
using MVPFramework.Presenter;
using System;
using TMPro;
using UISystem.MVP.Model;
using UISystem.MVP.View;
using UnityEngine;

namespace UISystem.MVP.Presenter
{
    internal class DescriptionPanelPresenter : MonoBehaviour,
        IPresenter<DescriptionPanelPresenter.DescriptionPanel, IModel<DescriptibleModel.Data>>,
        IPresenter<DescriptionPanelPresenter.DescriptionPanel, IModel<(string title, string description)>>
    {
        [Serializable]
        public struct DescriptionPanel
        {
            [field: SerializeField]
            public GameObject Root { get; private set; }

            [field: SerializeField]
            public TMP_Text Title { get; private set; }

            [field: SerializeField]
            public TMP_Text Description { get; private set; }

            public DescriptionPanel(GameObject root, TMP_Text title, TMP_Text description)
            {
                Root = root;
                Title = title;
                Description = description;
            }
        }

        public bool TryPresentElementWith(DescriptionPanel element, IModel<DescriptibleModel.Data> model)
        {
            DescriptibleTextView titleView = new DescriptibleTextView(
                new TextView(element.Title),
                new TextView(element.Description));

            (string title, string description) = model.Capture();
            return titleView.TryUpdateWith((title, description));
        }

        public bool TryPresentElementWith(DescriptionPanel element, IModel<(string title, string description)> model)
        {
            DescriptibleTextView titleView = new DescriptibleTextView(
                new TextView(element.Title),
                new TextView(element.Description));

            return titleView.TryUpdateWith(model.Capture());
        }
    }
}