namespace MVPFramework.Presenter
{
    public interface IObserverPresenter<in TView>
    {
        void ConnectTo(TView view);
        void DisconnectFrom(TView view);
    }
}