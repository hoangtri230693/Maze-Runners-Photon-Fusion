using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMultiPlayerManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject BackgroundMultiplayer;
    public GameObject BackgroundLoading;
    public TMP_InputField InputFieldName; 
    public GameObject[] NameCharacter;

    [Header("Network")]
    public NetworkConnection Network;

    private int currentCharacter = 0;

    private void Start()
    {
        if (Network != null)
        {
            Network = FindFirstObjectByType<NetworkConnection>();
        }
    }
    
    public void OnClickJoin()
    {
        string playerName = InputFieldName.text;
        if (string.IsNullOrEmpty(playerName))
        {
            return;
        }
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetInt("CharacterIndex", currentCharacter);
        PlayerPrefs.Save();

        if (Network != null)
        {
            Network.StartSharedMode();
            BackgroundLoading.SetActive(true);
        }
    }

    public void OnClickCancel()
    {
        SceneManager.LoadScene("StartGame");
    }

    public void OnClickNext()
    {
        NameCharacter[currentCharacter].SetActive(false);
        currentCharacter = (currentCharacter + 1) % NameCharacter.Length;
        NameCharacter[currentCharacter].SetActive(true);
    }

    public void OnClickPrevious()
    {
        NameCharacter[currentCharacter].SetActive(false);
        currentCharacter = (currentCharacter - 1 + NameCharacter.Length) % NameCharacter.Length;
        NameCharacter[currentCharacter].SetActive(true);
    }
}
