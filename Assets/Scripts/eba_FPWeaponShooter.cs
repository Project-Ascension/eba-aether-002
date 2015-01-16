using UnityEngine;
using System.Collections;

public class eba_FPWeaponShooter : vp_FPWeaponShooter {

	StatsScript m_statsScript;

	void Awake() 
	{
		m_statsScript = GameObject.Find("StatsBot").GetComponent<StatsScript>();

		base.Awake();
	}


	protected override void Fire()
	{
		m_statsScript.shotsFired = m_statsScript.shotsFired + 1;

		base.Fire();
	}
}
