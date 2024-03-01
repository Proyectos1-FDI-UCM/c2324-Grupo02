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

        public bool TryUpdateWith(string text)
        {
            _text.text = text;
            return true;
        }

        public bool TryUpdateWith(TMP_Text textMeshPro)
        {
            _text.text = textMeshPro.text;
            return true;
        }
    }
}