using System.Collections.Generic;
using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    [SerializeField] private List<Vector3> orbSpawnPoints = new();        // list of all spwanpoints of orbs
    [SerializeField] private GameObject orb;        // orb prefab

    [SerializeField] private GameManager gameManager;       // instance of game manager

    public void SpawnOrbs()
    {
        for (int i = 0; i < gameManager.totalOrbs; i++)
        {
            Instantiate(orb, orbSpawnPoints[i], orb.transform.rotation, transform); // create the physical object
        }
    }
}
