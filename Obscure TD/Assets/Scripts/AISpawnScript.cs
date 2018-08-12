using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawnScript : MonoBehaviour {

    //AI Prefabs.
    public GameObject AIprefab1;
    private GameObject cloneAIprefab1;

    //Spawn Timing.
    public float spawnInterval;
    public int spawnQuantity;
    private int spawnCounter;

    //Round Timing.
    public float roundDuration;
    private float roundTimer;
	

	void Start ()
    {
        //Setting variable values at the start.
        roundTimer = roundDuration;
        spawnCounter = 0;
	}
		
	void Update ()
    {
        //Round timer that dictates the start of a new spawn cycle.
        roundTimer -= Time.deltaTime;
        if (roundTimer <= 0)
        {
            StartSpawn();
        }
	}

    void StartSpawn ()
    {
        //Resets the round timer and then starts the repeated spawing of AI prefabs at a set interval.
        roundTimer = roundDuration;
        InvokeRepeating("Spawn", 0f, spawnInterval);
    }

    void Spawn ()
    {
        //Spawns in the AI prafab on the location of this scripts gameobject, then increments a counter that dictates the number of prefabs that will be spawned during each round.
        cloneAIprefab1 = Instantiate(AIprefab1, gameObject.transform.position, Quaternion.identity);
        spawnCounter++;
        if(spawnCounter == spawnQuantity)
        {
            CancelInvoke();
            spawnCounter = 0;
        }
    }
}
