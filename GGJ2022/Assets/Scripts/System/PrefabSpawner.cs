using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefab;
    public bool isNetworkSpawn;

    public void SpawnInPlace()
    {
        if (isNetworkSpawn && NetworkClient.isHostClient)
        {
            var obj = GameObject.Instantiate(prefab, this.transform.position, this.transform.rotation);
            NetworkServer.Spawn(obj);
            
        } else if (!isNetworkSpawn)
        {
            GameObject.Instantiate(prefab, this.transform.position, this.transform.rotation);
        }
    }
}
