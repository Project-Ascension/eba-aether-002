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
	public GameTypeEnum m_GameType;
	public int m_startingLives;
	public BastetFSM BastetState { get { return m_BastetState; } set { m_BastetState = value; } }

	// Private varibles
	protected BastetFSM m_BastetState;
	protected int m_CurrentLives;
	protected int m_Score;

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

	public int Score
	{
		get
		{
			return m_Score;
		}
		set
		{
			m_Score = value;
		}
	}

	StatsScript m_statsBot;

	// Use this for initialization
	void Start () {
		if (m_GameType == GameTypeEnum.LimitedLives)
		{
			// Initialize current lives with starting lives
			m_CurrentLives = m_startingLives;
		}
		else if (m_GameType == GameTypeEnum.UnlimitedLives)
		{
			m_CurrentLives = 0;
		}

		m_statsBot = GameObject.Find("StatsBot").GetComponent<StatsScript>();
		if (m_statsBot == null)
			Debug.LogError("Bastet could not find a StatsScript object on statsBot!");
	}
	
	// Update is called once per frame
	void Update () {


		switch (BastetState)
		{
		case BastetFSM.Playing:
			// Only if there are limited lives
			if (m_GameType == GameTypeEnum.LimitedLives)
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
//		if (m_GameType == GameTypeEnum.UnlimitedLives)
//		{
//			int kills = m_statsBot.GetKills();
//			int deaths = m_statsBot.GetDeaths();
//			int shots = m_statsBot.shotsFired;
//			int score = (kills * 100 + (deaths * 300) - (shots * 10));
//
//			if (score < 0)
//			{
//				return 0;
//			}
//
//			return score;
//
//		}
//		else
//		{
//			return -1;
//		}

		return Score;
	}

//	public string CalculateScoreString(bool debug = false)
//	{
//		if (debug == true)	
//		{
//			string score = "";
//			score += "Kills: " + (m_statsBot.GetKills() * 100).ToString() + "\n";
//			score += "Deaths: -" + (m_statsBot.GetDeaths() * 300).ToString() + "\n";
//			score += "Shots: -" + (m_statsBot.shotsFired * 10).ToString() + "\n";
//			score += "Total: " + CalculateScore();
//			return score;
//		}
//		else
//		{
//			return CalculateScore().ToString();
//		}
//	}

	public void AddScore(int baseScore)
	{
		Score += baseScore * m_statsBot.GetCurrentMultiplier();
	}
}
