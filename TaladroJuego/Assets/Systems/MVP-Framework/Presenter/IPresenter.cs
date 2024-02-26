using MVPFramework.Model;

namespace MVPFramework.Presenter
{
    public interface IPresenter<in TElement, in TModel>
    {
        bool TryPresentElementWith(TElement element, TModel model);
    }
}