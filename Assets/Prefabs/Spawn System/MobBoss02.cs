using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobBoss02 : MonoBehaviour {
	// Public variables //

	// Private variables //
	// Creating a list to store the Spawn Points
	private List<SpawnPoint> m_registeredSpawnPoints;
	// Creating an enum to easily store the state
	enum StateFSM 
	{
		Idle,
		SpawnSetup,
		Spawning
	}
	// Creating a variable to store the state of the FSM
	StateFSM m_state;

	// Use this for initialization
	void Start () {
		// Initializing m_state
		m_state = StateFSM.Idle;
	}
	
	// Update is called once per frame
	void Update () {

	}

	// Called by individual SpawnPoints on their Awake function

}
