/////////////////////////////////////////////////////////////////
/// 
/// is kalled bastet casue basted was eedjipshun cat godes 
/// and casts has nein lifs and tihs thyng cownts livse ok?
///
/////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class Bastet : MonoBehaviour {

	// Public variables
	public int m_startingLives;
	public BastetFSM BastetState { get { return m_BastetState; } set { m_BastetState = value; } }

	// Private varibles
	protected BastetFSM m_BastetState;
	protected int m_CurrentLives;

	public enum BastetFSM
	{
		Playing,
		Lost,
		Won
	}

	// Use this for initialization
	void Start () {
		// Initialize current lives with starting lives
		m_CurrentLives = m_startingLives;
	}
	
	// Update is called once per frame
	void Update () {

		switch (BastetState)
		{
		case BastetFSM.Playing:
			// Check to see if lives are gone
			if (m_CurrentLives < 1)
			{
				BastetState = BastetFSM.Lost;
				break;
			}

			break;

		case BastetFSM.Lost:
			Debug.Log("You lose. :(");
			break;

		case BastetFSM.Won:
			Debug.Log("You Win!");
			break;
		}
	}

	public void SetWin()
	{
		BastetState = BastetFSM.Won;
	}

	public void SetLose()
	{
		BastetState = BastetFSM.Lost;
	}

	public void LoseLife()
	{
		m_CurrentLives--;
	}

	public void AddLife()
	{
		m_CurrentLives++;
	}

	public int GetLives()
	{
		return m_CurrentLives;
	}
}
