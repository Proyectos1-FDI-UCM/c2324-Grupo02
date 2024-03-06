namespace MVPFramework.Presenter
{
    public interface IPresenter<in TElement, in TModel>
    {
        bool TryPresentElementWith(TElement element, TModel model);
    }

    public interface IPresenter<in TElement, in TModel, out TView>
    {
        TView PresentElementWith(TElement element, TModel model1);
    }
}