using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MobBoss : MonoBehaviour {
	// Public variables //
	// Creating a enum for even or random spawn distribution
	public SpawnDistributionEnum spawnDistributionType;
	// Creating a list to store all Waves
	public List<Wave> WaveList = new List<Wave>(); 
	// Creating a struct for waves
	[Serializable]
	public class Wave
	{
		public List<Platoon> PlatoonList;
		public float WarmUp = 0f;
	}
	// Creating a struct for platoons
	[Serializable]
	public class Platoon
	{
		// mobPrefab holds the prefab for the platoon
		public GenericMob mobPrefab;
		
		// mobCount holds the number of mobs to spawn in the platoon
		public int mobCount;
		
		// platoonSpawnPoint holds a reference to the platoon's
		// spawn point
		[HideInInspector]
		public SpawnPoint platoonSpawnPoint;
	}

	// Private variables //
	// Creating a list to store the Spawn Points
	private List<SpawnPoint> m_registeredSpawnPoints = new List<SpawnPoint>();
	// Creating a list to store all Spawn Points with Platoons referencing them
	private List<SpawnPoint> m_spawnPointsWithPlatoons = new List<SpawnPoint>();
	// Creating a list to store all active enemies
	private List<GenericMob> m_registeredMobs = new List<GenericMob>();
	// Creating a Wave to store the current wave
	private Wave m_activeWave;
	// Creating a variable to store the current wave's warm up time in seconds
	private float m_warmUpTimer;
	// Creating an enum to easily store the state
	[HideInInspector]
	public enum StateFSM 
	{
		Idle,
		LastWaveIdle,
		WarmingUp,
		SpawnSetup,
		Spawning,
		WinState
	}
	// Creating a variable to store the state of the FSM
	private StateFSM m_state;
	// Creating an enum to easily store the type of spawning distribution
	public enum SpawnDistributionEnum
	{
		EvenDistribution,
		RandomDistribution
	}


	// Use this for initialization
	void Start () {
		// Initializing m_state
		m_state = StateFSM.WarmingUp;
		// Initializing the active wave
		m_activeWave = WaveList[0];

		// Check to see if all wave platoons have prefabs and have
		// mobCount values greater than zero
		// TODO
	}
	
	// Update is called once per frame
	void Update () {
		// Create FSM
		switch (m_state)
		{
			// If idle:
		case StateFSM.Idle:
			if (GetActiveEnemyCount() < 1)
			{
				// Set state to SetupSpawn
//				Debug.Log("No active enemies. Switching FSM to WarmingUp.");
				m_state = StateFSM.WarmingUp;
			}
//			else
//			{
//				Debug.Log("There are " + GetActiveEnemyCount() + " enemies remaining.");
//			}
			break;
		case StateFSM.LastWaveIdle:
			// Check to see if the last wave is over and set state to WinState
			if (GetActiveEnemyCount() < 1)
			{
//				Debug.Log("No active enemies. Last wave. Switching FSM to WinState.");
				m_state = StateFSM.WinState;
			}
			break;
		case StateFSM.WarmingUp:
//			Debug.Log("FSM is in WarmingUp");
			// If m_warmUpTimer has no value, get the value (first loop in this state)
			if (m_warmUpTimer == 0f)
			{
				m_warmUpTimer = GetWarmUpTime(m_activeWave);
			}

//			Debug.Log(GetSecondsToNextWave());

			// If the current time has surpassed the time for the next wave, set m_warmUpTimer to null
			// and change the state to SpawnSetup
			if (Time.time >= m_warmUpTimer)
			{
				m_warmUpTimer = 0f;
				m_state = StateFSM.SpawnSetup;
			}
			break;
		case StateFSM.SpawnSetup:
//			Debug.Log("FSM is in SpawnSetup.");

			// Looping through all platoons in the active wave and assigning each a spawn point
			if (spawnDistributionType == SpawnDistributionEnum.RandomDistribution)
			{
				RandomSpawnDistributor(m_activeWave);
			}
			else if (spawnDistributionType == SpawnDistributionEnum.EvenDistribution)
			{
//				Debug.Log ("spawnDistributionType is set to EvenDistribution");
				EvenSpawnDistributor();
			}
			else 
			{
//				Debug.LogError("spawnDistributionType is set to an unknown value: " + spawnDistributionType);
			}

//			Debug.Log("Number of spawn points with platoons: " + m_spawnPointsWithPlatoons.Count);

			// Set state to Spawning
//			Debug.Log("All platoons in the wave have been given a spawn point. Switching FSM to Spawning.");
			m_state = StateFSM.Spawning;
			break;
		case StateFSM.Spawning:
//			Debug.Log ("FSM is in Spawining.");
			// Loop through all spawn points with platoons still on them
			for (int i = 0; i < m_spawnPointsWithPlatoons.Count; i++)
			{
				SpawnPoint currentSpawnPoint = m_spawnPointsWithPlatoons[i];

				if (currentSpawnPoint.GetUnspawnedPlatoonCount() > 0)
				{
					// Get the topmost platoon and spawn one of it's mobs
					Platoon currentPlatoon = currentSpawnPoint.PeekAtCurrentUnspawnedPlatoon();

					if (currentPlatoon.mobCount > 0)
					{
						currentSpawnPoint.SpawnMob(currentPlatoon.mobPrefab);

						// Decrease the number of mobs in that platoon by one
						currentPlatoon.mobCount = currentPlatoon.mobCount - 1;

//						Debug.Log ("Spawning " + currentPlatoon.mobPrefab.name + " at spawn point " + 
//						           currentSpawnPoint.GetInstanceID() + " with " + currentPlatoon.mobCount
//						           + " prefabs left to spawn");
					}


					// If the topmost platoon no longer has any mobs left to spawn,
					// dequeue it from the spawn point
					if (currentPlatoon.mobCount < 1)
					{
						currentSpawnPoint.DequeueTopPlatoon();
					}
				}

				// If the currentSpawnPoint no longer has any unspawned platoons on it,
				// remove it from m_spawnPointsWithPlatoons
				else
				{
					m_spawnPointsWithPlatoons.Remove(currentSpawnPoint);
				}
			}

			// If there are no more spawn points with platoons, set FSM to Idle
			if (m_spawnPointsWithPlatoons.Count < 1)
			{

				// If there are still more waves, make the next wave m_activeWave
				if (WaveList.IndexOf(m_activeWave) + 1 < WaveList.Count)
				{
					m_state = StateFSM.Idle;
					m_activeWave = WaveList[WaveList.IndexOf(m_activeWave) + 1];
				}
				else
				{
					m_state = StateFSM.LastWaveIdle;
				}
			}

			break;
		case StateFSM.WinState:
//			Debug.Log("You WIN!!!");
			break;
		}
	}

	// Used to get the state of the MobBoss from outside
	public StateFSM MobBossState
	{
		get
		{
			return m_state;
		}
	}

	// Called by individual SpawnPoints on their Awake function
	public void RegisterSpawnPoint(SpawnPoint sp)
	{
		m_registeredSpawnPoints.Add(sp);
	}

	public int GetSpawnIndex(SpawnPoint sp)
	{
		return m_registeredSpawnPoints.IndexOf(sp);
	}

	// Called by individual SpawnPoints to remove them from the m_registeredSpawnPoints list
	public void DeregisterSpawnPoint(SpawnPoint sp)
	{
		m_registeredSpawnPoints.Remove(sp);
	}

	// Called by GenericMob on Awake function
	public void RegisterMob(GenericMob mob)
	{
		m_registeredMobs.Add(mob);
	}

	// Called by GenericMob on Destroy function
	public void DeRegisterMob(GenericMob mob)
	{
		m_registeredMobs.Remove(mob);
	}

	public int GetActiveEnemyCount()
	{
		int count = m_registeredMobs.Count;
		return count;
	}

	public SpawnPoint GetRandomSpawnPoint() 
	{
		int randomInt = UnityEngine.Random.Range(0, m_registeredSpawnPoints.Count);
		return m_registeredSpawnPoints[randomInt];
	}

	public void RandomSpawnDistributor(Wave currentWave)
	{
		// Loop through platoons in currentWave
		for (int n = 0; n < currentWave.PlatoonList.Count; n++)
		{
			// Set the platoon to be acted on
			Platoon currentPlatoon = currentWave.PlatoonList[n];
			// Get random spawn point for platoon
			SpawnPoint chosenSpawnPoint = GetRandomSpawnPoint();
			currentPlatoon.platoonSpawnPoint = chosenSpawnPoint;
			// Enqueue currentPlatoon with it's spawn point 
			chosenSpawnPoint.EnqueueUnspawnedPlatoon(currentPlatoon);

			// If the platoon's spawn point has not been added to
			// m_spawnPointsWithPlatoons, add it
			if (!m_spawnPointsWithPlatoons.Contains(chosenSpawnPoint))
			{
				m_spawnPointsWithPlatoons.Add (chosenSpawnPoint);
			}

//			Debug.Log("The spawn point of mob '" + currentPlatoon.mobPrefab.name + "' is : " + GetSpawnIndex(chosenSpawnPoint));
		}
	}

	public void EvenSpawnDistributor() 
	{
//		Debug.LogWarning("Even spawning distribution is not set up yet!");
	}

	public float GetWarmUpTime(Wave w)
	{
		return Time.time + w.WarmUp;
	}

	public int GetSecondsToNextWave()
	{
		if (m_warmUpTimer != null)
		{
			return (int)Math.Round(m_warmUpTimer - Time.time, 0);
		}
		return 0;
	}
}
