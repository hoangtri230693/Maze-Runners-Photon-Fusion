using UnityEngine;
using Fusion;

public class GateController : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            playerStats.IsWinner = true;
            GameManager.Instance.EndGameRpc();
            Destroy(gameObject);
        }
    }
}