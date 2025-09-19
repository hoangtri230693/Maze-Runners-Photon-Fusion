using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;

public class NetworkSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject[] CharacterPrefabs;
    
    public void PlayerJoined(PlayerRef player)
    {
        if (SceneManager.GetActiveScene().name != "PlayGame") return;

        if (player == Runner.LocalPlayer)
        {
            int characterIndex = PlayerPrefs.GetInt("CharacterIndex", 0);
            characterIndex = Mathf.Clamp(characterIndex, 0, CharacterPrefabs.Length - 1);
            Runner.Spawn(CharacterPrefabs[characterIndex], new Vector3(5.0f, 1.0f, 4.0f), Quaternion.identity);
        }
    }
}
