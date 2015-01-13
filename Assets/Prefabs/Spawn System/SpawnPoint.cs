using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPoint : MonoBehaviour {
	// Public variables //

	// Private variables //
	// Holds the MobBoss
	private MobBoss m_MobBoss;
	// Holds all platoons that have not finished their spawning
	private Queue<MobBoss.Platoon> m_unspawnedPlatoonQueue = new Queue<MobBoss.Platoon>();

	void Awake () {
		// Grab the mobBoss GameObject
		GameObject mobBossObject = GameObject.Find("MobBoss");
		if (!mobBossObject) {
			Debug.LogWarning("Can't find GameObject 'MobBoss'");
		}

		// Find the MobBoss monobehaviour component
		m_MobBoss = mobBossObject.GetComponent<MobBoss>();
		if (!m_MobBoss) {
			Debug.LogWarning("Can't find component of type MobBoss in 'mobBossObject'");
		}

		// Register this SpawnPoint with the MobBoss 
		m_MobBoss.RegisterSpawnPoint(this);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool SpawnMob (GenericMob mob) {
		GenericMob mobClone = (GenericMob) Instantiate(mob, transform.position, transform.rotation);
		if (mobClone)
		{
			return true;
		}
		return false;
	}

	public MobBoss.Platoon PeekAtCurrentUnspawnedPlatoon()
	{
		return m_unspawnedPlatoonQueue.Peek();
	}

	public void DequeueTopPlatoon()
	{
		m_unspawnedPlatoonQueue.Dequeue();
	}

	public void EnqueueUnspawnedPlatoon(MobBoss.Platoon platoon)
	{
		m_unspawnedPlatoonQueue.Enqueue(platoon);
	}

	public int GetUnspawnedPlatoonCount()
	{
		return m_unspawnedPlatoonQueue.Count;
	}
}
