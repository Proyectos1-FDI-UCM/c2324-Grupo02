using MVPFramework.View;
using System;
using UnityEngine;
using static UISystem.MVP.View.DescriptibleTextView;

namespace UISystem.MVP.View
{
    internal readonly struct DescriptibleTextView : IView<TitledText>, IView<string>
    {
        [Serializable]
        public struct TitledText
        {
            [field: SerializeField]
            public string Title { get; private set; }

            [field: SerializeField]
            [field: TextArea]
            public string Text { get; private set; }

            public TitledText(string title, string text)
            {
                Title = title;
                Text = text;
            }
        }

        private readonly IView<string> _titleView;
        private readonly IView<string> _descriptionView;

        private readonly TitledText _defaulted;

        public DescriptibleTextView(IView<string> titleView, IView<string> descriptionView)
        {
            _titleView = titleView;
            _descriptionView = descriptionView;
            _defaulted = new TitledText(string.Empty, string.Empty);
        }

        public DescriptibleTextView(IView<string> titleView, IView<string> descriptionView, TitledText defaulted)
        {
            _titleView = titleView;
            _descriptionView = descriptionView;
            _defaulted = defaulted;
        }

        public bool TryUpdateWith(TitledText status) =>
            _titleView.TryUpdateWith(status.Title) && _descriptionView.TryUpdateWith(status.Text);

        public bool TryUpdateWith(string status) =>
            _titleView.TryUpdateWith(status) && _descriptionView.TryUpdateWith(_defaulted.Text);
    }
}