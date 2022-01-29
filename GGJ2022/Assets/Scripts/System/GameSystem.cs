using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameSystem : NetworkBehaviour
{
    #region Singleton shit
    public static GameSystem Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.Log("Why are there two game systems? So dumb.");
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
    #endregion

    public List<uPlayer> players = new List<uPlayer>();

    public Transform utopiaSpot, dystopiaSpot;

    public void AddPlayer(uPlayer player)
    {
        players.Add(player);
    }

    [Server]
    public void Swap(uPlayer player)
    {
        Debug.Log($"Swap participating: {player.name}");
        if (players.TrueForAll(p => p.wantsToSwap))
        {
            if (players.Count == 1)
            {
                players[0].wantsToSwap = false;
                Debug.Log("Player moving to another reality");
                if (players[0].isInUtopia)
                {
                    players[0].isInUtopia = false;
                    players[0].RpcMoveTo(players[0].netIdentity.connectionToClient, dystopiaSpot.position);
                } else
                {
                    players[0].isInUtopia = true;
                    players[0].RpcMoveTo(players[0].netIdentity.connectionToClient, utopiaSpot.position);
                }
            }
        }
    }
}
