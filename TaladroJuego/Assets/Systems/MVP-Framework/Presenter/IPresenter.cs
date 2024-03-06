namespace MVPFramework.Presenter
{
    public interface IPresenter<in TUpdate>
    {
        bool TryUpdateWith(TUpdate status);
    }
}