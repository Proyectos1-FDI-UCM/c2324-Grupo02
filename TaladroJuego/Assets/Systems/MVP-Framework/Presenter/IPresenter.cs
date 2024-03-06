namespace MVPFramework.Presenter
{
    public interface IPresenter<in TView>
    {
        bool TryUpdate(TView view);
    }

    public interface IPresenter<in TView, in TModel>
    {
        bool TryUpdate(TView view, TModel model);
    }
}