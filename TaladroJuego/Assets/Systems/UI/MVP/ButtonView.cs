using MVPFramework.View;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UISystem.MVP
{
    internal readonly struct ButtonView //: IView<>
    {
        private readonly Button _button;

        public ButtonView(Button button, UnityAction pressed)
        {
            _button = button;
            button.onClick.AddListener(pressed);
        }

        public ButtonView(Button button)
        {
            _button = button;
            //button.
        }
    }
}