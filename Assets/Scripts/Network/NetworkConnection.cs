using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkConnection : MonoBehaviour
{
    public static NetworkConnection Instance;
    public NetworkRunner _networkRunner;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async void StartSharedMode()
    {
        var _runner = Instantiate(_networkRunner);
        _runner.ProvideInput = true;

        var sceneRef = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex + 1);

        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = "MyRoom",
            Scene = sceneRef,
        });
    }
}
