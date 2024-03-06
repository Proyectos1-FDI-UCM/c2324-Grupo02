using MVPFramework.Model;
using MVPFramework.Presenter;
using MVPFramework.View;
using UnityEngine;
using static UISystem.MVP.Model.VisualDescriptible;
using static UISystem.MVP.View.IconLabelView;

namespace UISystem.MVP.Presenter
{
    internal class LabeledIconPresenter : MonoBehaviour, IPresenter<IView<LabeledSprite>, IModel<PortrayedDescription>>
    {
        public bool TryUpdate(IView<LabeledSprite> view, IModel<PortrayedDescription> model)
        {
            PortrayedDescription portrayedDescription = model.Capture();
            return view.TryUpdateWith(new LabeledSprite(portrayedDescription.sprite, portrayedDescription.title));
        }
    }
}