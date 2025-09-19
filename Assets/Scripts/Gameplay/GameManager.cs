using UnityEngine;
using Fusion;
using System.Linq;
using UnityEngine.SceneManagement;

public enum GameState
{
    Waiting,
    Playing,
    Ended
}

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;

    [Networked] public GameState State { get; set; }

    public override void Spawned()
    {
        Instance = this;
        if (Object.HasStateAuthority)
        {
            State = GameState.Waiting;
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (State == GameState.Waiting && Runner.ActivePlayers.Count() >= 2 && Object.HasStateAuthority)
        {
            StartGameRpc();
        }
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void StartGameRpc()
    {
        State = GameState.Playing;
        Debug.Log("Game Started!");
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void EndGameRpc()
    {
        State = GameState.Ended;
        foreach (var player in FindObjectsByType<PlayerStats>(FindObjectsSortMode.None))
        {
            player.TotalTime();
        }

        var result = FindFirstObjectByType<UIPlayGameManager>();
        if (result != null)
        {
            result.ShowResultPanel();
        }
        Debug.Log("End Game!");

        Invoke(nameof(ReturnToMainMenu), 10f);
    }

    public async void ReturnToMainMenu()
    {
        //Disconnect all players and return to main menu in Shared mode Photon
        if (Object.HasStateAuthority) // Chỉ có State Authority mới thực hiện việc này 
        // //StateAuthority là server trong Shared mode 
        {
            await Runner.Shutdown();
            SceneManager.LoadScene("StartGame", LoadSceneMode.Single);
        }
        // Các client khác sẽ tự động trở về main menu khi kết nối bị mất
    }
}
