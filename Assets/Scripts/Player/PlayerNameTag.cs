using TMPro;
using UnityEngine;
using Fusion;

public class PlayerNameTag : NetworkBehaviour
{
    [Networked] public NetworkString<_16> PlayerName { get; set; }
    public TextMeshProUGUI NameText;
    public Camera _mainCamera;

    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            _mainCamera = Camera.main;
            PlayerName = PlayerPrefs.GetString("PlayerName", "Player");
        }
        NameText.text = PlayerName.ToString();
    }

    private void LateUpdate()
    {
        if (_mainCamera != null && NameText != null)
        {
            NameText.transform.LookAt(NameText.transform.position + _mainCamera.transform.rotation * Vector3.forward,
                                        _mainCamera.transform.rotation * Vector3.up);
        }
    }
}
