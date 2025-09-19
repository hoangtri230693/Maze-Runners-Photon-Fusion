using UnityEngine;
using Fusion;

public class PlayerStats : NetworkBehaviour
{
    [Networked] public string PlayerName { get; set; }
    [Networked] public float FinishTime { get; set; }
    [Networked] public bool IsWinner { get; set; }

    [Networked] private double StartTime { get; set; }


    public override void Spawned()
    {
        base.Spawned();
        if (HasStateAuthority)
        {
            PlayerName = PlayerPrefs.GetString("PlayerName", "Player");
            FinishTime = 0f;
            IsWinner = false;
            StartTime = Runner.SimulationTime;
        }
    }

    public void TotalTime()
    {
        if (FinishTime == 0)
        {
            FinishTime = (float)(Runner.SimulationTime - StartTime);
        }
    }
}
