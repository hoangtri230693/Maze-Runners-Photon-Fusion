using TMPro;
using UnityEngine;

public class UIPlayGameManager : MonoBehaviour
{
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private GameObject _horizontalResult;
    [SerializeField] private Transform _resultContent;
    [SerializeField] private GameObject _leaveRoomPanel;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_leaveRoomPanel.activeSelf)
            {
                _leaveRoomPanel.SetActive(false);
            }
            else
            {
                _leaveRoomPanel.SetActive(true);
            }
        }
    }
    
    public void ShowResultPanel()
    {
        _resultPanel.SetActive(true);

        //Xóa các row cũ
        foreach (Transform child in _resultContent)
        {
            Destroy(child.gameObject);
        }

        //Lấy danh sách PlayerStats
        var players = FindObjectsByType<PlayerStats>(FindObjectsSortMode.InstanceID);

        //Tao các row mới
        foreach (var player in players)
        {
            var row = Instantiate(_horizontalResult, _resultContent);

            var texts = row.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = player.PlayerName;
            texts[1].text = player.IsWinner ? "Winner" : "Loser";
            texts[2].text = player.FinishTime.ToString("F2") + "s";
        }
    }

    public void OnClickSettings()
    {
        _leaveRoomPanel.SetActive(true);
    }

    public void OnClickYes()
    {
        GameManager.Instance.ReturnToMainMenu();
    }

    public void OnClickNo()
    {
        _leaveRoomPanel.SetActive(false);
    }
}
