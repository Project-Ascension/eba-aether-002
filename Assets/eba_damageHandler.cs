using UnityEngine;
using System.Collections;

public class eba_damageHandler : vp_DamageHandler {

	private StatsScript m_statsScript;
	private MobBoss m_mobBoss;
	private GenericMob m_thisMob;
	protected vp_Timer.Handle m_DestroyTimer = new vp_Timer.Handle();

	// Use this for initialization
	void Start () {
		m_statsScript = GameObject.Find("StatsBot").GetComponent<StatsScript>();
		m_mobBoss = GameObject.Find("MobBoss").GetComponent<MobBoss>();
		m_thisMob = gameObject.GetComponent<GenericMob>();

		if (!m_thisMob)
		{
			Debug.LogError("No GenericMob Script on MobBoss");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Overrides the Die() function from vp_DamageHandler
	public virtual void Die()
	{
		m_statsScript.AddKill();

		base.Die();

		m_mobBoss.DeRegisterMob(m_thisMob);

//		Destroy(gameObject);
	}
}
