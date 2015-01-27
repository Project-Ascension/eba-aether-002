using UnityEngine;
using System.Collections;

public class GenericMob : MonoBehaviour {

	GameObject m_mobBoss;
	MobBoss m_boss;

	public int scoreValue = 100;

	void Awake () {
		m_mobBoss = GameObject.Find("MobBoss");
		if (!m_mobBoss){
			Debug.Log("Could not find GameObject with name 'MobBoss'");
			return;
		}

		m_boss = m_mobBoss.GetComponent<MobBoss>();
		if (!m_boss) {
			Debug.Log ("Could not find MobBoss component on Gameobject 'MobBoss'");
			return;
		}

		m_boss.RegisterMob(this);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDestroy () {
		m_boss.DeRegisterMob(this);
	}
}
