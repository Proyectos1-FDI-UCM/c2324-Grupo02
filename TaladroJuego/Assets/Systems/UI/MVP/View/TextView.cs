using MVPFramework.View;
using TMPro;

namespace UISystem.MVP.View
{
    internal readonly struct TextView : IView<string>, IView<TMP_Text>
    {
        private readonly TMP_Text _text;

        public TextView(TMP_Text text)
        {
            _text = text;
        }

        public bool TryUpdateWith(string model)
        {
            _text.text = model;
            return true;
        }

        public bool TryUpdateWith(TMP_Text model)
        {
            _text.text = model.text;
            return true;
        }
    }
}