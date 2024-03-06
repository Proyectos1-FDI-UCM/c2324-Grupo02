using MVPFramework.Model;
using MVPFramework.Presenter;
using MVPFramework.View;
using UnityEngine;
using static UISystem.MVP.Model.Descriptible;
using static UISystem.MVP.View.DescriptibleTextView;

namespace UISystem.MVP.Presenter
{
    internal class DescriptionPanelPresenter : MonoBehaviour, IPresenter<IView<TitledText>, IModel<TitledDescription>>
    {
        public bool TryUpdate(IView<TitledText> view, IModel<TitledDescription> model)
        {
            TitledDescription description = model.Capture();
            return view.TryUpdateWith(new TitledText(description.title, description.description));
        }
    }
}