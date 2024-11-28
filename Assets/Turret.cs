using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Turret : MonoBehaviour
{
    private NavMeshAgent navAgent;  
    private GameObject player;      
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        navAgent.SetDestination(player.transform.position);
    }
}
