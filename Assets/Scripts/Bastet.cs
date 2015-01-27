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
	public GameTypeEnum GameType;
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

	public enum GameTypeEnum
	{
		LimitedLives,
		UnlimitedLives
	}

	StatsScript m_statsBot;

	// Use this for initialization
	void Start () {
		if (GameType == GameTypeEnum.LimitedLives)
		{
			// Initialize current lives with starting lives
			m_CurrentLives = m_startingLives;
		}
		else if (GameType == GameTypeEnum.UnlimitedLives)
		{
			m_CurrentLives = 0;
		}

		m_statsBot = GameObject.Find("StatsBot").GetComponent<StatsScript>();
		if (m_statsBot == null)
			Debug.LogError("Bastet could not find a StatsScript object on statsBot!");
	}
	
	// Update is called once per frame
	void Update () {

//		Debug.Log("m_currentLives: " + m_CurrentLives);

		switch (BastetState)
		{
		case BastetFSM.Playing:
			// Only if there are limited lives
			if (GameType == GameTypeEnum.LimitedLives)
			{
				// Check to see if lives are gone
				if (m_CurrentLives < 1)
				{
					BastetState = BastetFSM.Lost;
					break;
				}
			}

			break;

		case BastetFSM.Lost:
			Debug.Log("You lose. :(");
			Time.timeScale = 0;
			break;

		case BastetFSM.Won:
//			Debug.Log("You Win!");
			Time.timeScale = 0;
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

	public BastetFSM GetState()
	{
		return m_BastetState;
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

	public int CalculateScore()
	{
		if (GameType == GameTypeEnum.UnlimitedLives)
		{
			int kills = m_statsBot.GetKills();
			int deaths = m_statsBot.GetDeaths();
			int shots = m_statsBot.shotsFired;
			int score = (kills * 10 - (deaths * 30) - shots) * 10;

			if (score < 0)
			{
				return 0;
			}

			return score;

		}
		else
		{
			return -1;
		}
	}
}
