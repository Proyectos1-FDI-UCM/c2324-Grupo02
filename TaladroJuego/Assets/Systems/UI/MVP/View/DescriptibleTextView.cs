using MVPFramework.View;

namespace UISystem.MVP.View
{
    internal readonly struct DescriptibleTextView : IView<(string name, string description)>
    {
        private readonly IView<string> _nameView;
        private readonly IView<string> _descriptionView;

        public DescriptibleTextView(IView<string> nameView, IView<string> descriptionView)
        {
            _nameView = nameView;
            _descriptionView = descriptionView;
        }

        public bool TryUpdateWith((string name, string description) model) =>
            _nameView.TryUpdateWith(model.name) && _descriptionView.TryUpdateWith(model.description);
    }
}