using UnityEngine;
using System.Collections;

public class eba_FPPlayerDamageHandler : vp_FPPlayerDamageHandler {

	private StatsScript m_statsScript;

	void Start ()
	{
		m_statsScript = GameObject.Find("StatsBot").GetComponent<StatsScript>();
	}

	public override void Die()
	{
		
		base.Die();
		
		m_statsScript.AddDeath();
		
	}
}
