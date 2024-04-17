using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = SCENE_LOADER_REQUEST_NAME, menuName = SCENE_LOADER_REQUEST_PATH)]
public class SceneLoaderRequesterObject : ScriptableObject
{
    private const string SCENE_LOADER_REQUEST_NAME = "Scene Loader Requester";
    private const string SCENE_LOADER_REQUEST_PATH = "Game State/" + SCENE_LOADER_REQUEST_NAME;

    [SerializeField]
    private LoadSceneParameters _loadSceneParameters = new LoadSceneParameters()
    {
        loadSceneMode = LoadSceneMode.Single,
        localPhysicsMode = LocalPhysicsMode.None,
    };

    public float CurrentProgress { get; private set; } = 0.0f;

    [field: SerializeField]
    public UnityEvent SceneLoadRequested { get; private set; }

    public void RequestSceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName, _loadSceneParameters);
        SceneLoadRequested?.Invoke();
    }

    public void RequestSceneLoad(int sceneBuildIndex)
    {
        SceneManager.LoadScene(sceneBuildIndex, _loadSceneParameters);
        SceneLoadRequested?.Invoke();
    }

    public async Task RequestSceneLoadAsync(string sceneName)
    {
        Debug.Log(sceneName);

        Application.backgroundLoadingPriority = ThreadPriority.Low;
        AsyncOperation asyncLoadOperation = SceneManager.LoadSceneAsync(sceneName, _loadSceneParameters);
        SceneLoadRequested?.Invoke();

        while (!asyncLoadOperation.isDone)
        {
            CurrentProgress = asyncLoadOperation.progress;
            await Task.Yield();
        }

        CurrentProgress = 1.0f;
    }

    public async Task RequestSceneLoadAsync(int sceneBuildIndex)
    {
        Application.backgroundLoadingPriority = ThreadPriority.Low;
        AsyncOperation asyncLoadOperation = SceneManager.LoadSceneAsync(sceneBuildIndex, _loadSceneParameters);
        SceneLoadRequested?.Invoke();

        while (!asyncLoadOperation.isDone)
        {
            CurrentProgress = asyncLoadOperation.progress;
            await Task.Yield();
        }

        CurrentProgress = 1.0f;
    }

    public async void RequestAsyncSceneLoad(string sceneName) => await RequestSceneLoadAsync(sceneName);
    public async void RequestAsyncSceneLoad(int sceneBuildIndex) => await RequestSceneLoadAsync(sceneBuildIndex);
}
