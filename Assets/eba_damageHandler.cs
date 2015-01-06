using UnityEngine;
using System.Collections;

public class eba_damageHandler : vp_DamageHandler {

	private StatsScript _statsScript;
	private MobBoss _mobBoss;
	private GenericMob _thisMob;
	protected vp_Timer.Handle m_DestroyTimer = new vp_Timer.Handle();

	// Use this for initialization
	void Start () {
		_statsScript = GameObject.Find("StatsBot").GetComponent<StatsScript>();
		_mobBoss = GameObject.Find("MobBoss").GetComponent<MobBoss>();
		_thisMob = gameObject.GetComponent<GenericMob>();

		if (!_thisMob)
		{
			Debug.LogError("No GenericMob Script on GameObject");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Overrides the Die() function from vp_DamageHandler
	public virtual void Die()
	{
		_statsScript.AddKill();

		base.Die();

		_mobBoss.DeRegisterMob(_thisMob);

//		Destroy(gameObject);
	}
}
