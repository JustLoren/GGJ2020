using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;

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
        if (players.Count > 1)
        {
            player.isInUtopia = !players[0].isInUtopia;
            StartCoroutine(DelayedTeleportTo(player, player.isInUtopia ? utopiaSpot : dystopiaSpot));
        }
        else
        {
            //first player ... delayed teleport?
            player.isInUtopia = true;
            StartCoroutine(DelayedTeleportTo(player, utopiaSpot));
        }
    }

    [Server]
    private IEnumerator DelayedTeleportTo(uPlayer player, Transform spot)
    {
        yield return new WaitForSeconds(.1f);
        TeleportTo(player, spot);
    }

    [Server]
    private void TeleportTo(uPlayer player, Transform spot)
    {
        player.RpcMoveTo(player.netIdentity.connectionToClient, spot.position, spot.rotation, player.cameraObject.localRotation);
    }

    [Server]
    public void Swap(uPlayer player)
    {
        Debug.Log($"Swap participating: {player.name}");
        if (players.TrueForAll(p => p.wantsToSwap))
        {
            if (players.Count == 1)
            {
                player.wantsToSwap = false;
                Debug.Log("Player moving to another reality");
                player.isInUtopia = !player.isInUtopia;
                TeleportTo(player, player.isInUtopia ? utopiaSpot : dystopiaSpot);
            } 
            else
            {
                players[0].wantsToSwap = false;
                players[1].wantsToSwap = false;

                players[0].isInUtopia = !players[0].isInUtopia;
                players[1].isInUtopia = !players[1].isInUtopia;

                players[0].RpcMoveTo(players[0].netIdentity.connectionToClient, players[1].transform.position, players[1].transform.rotation, players[1].cameraObject.localRotation);
                players[1].RpcMoveTo(players[1].netIdentity.connectionToClient, players[0].transform.position, players[0].transform.rotation, players[0].cameraObject.localRotation);
            }
        } else
        {
            var otherPlayer = players.Where(p => p != player).Single();
            otherPlayer.RpcNotifySwap(otherPlayer.netIdentity.connectionToClient);
        }
    }
}
