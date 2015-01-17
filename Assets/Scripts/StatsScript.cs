using UnityEngine;
using System.Collections;

public class StatsScript : MonoBehaviour {
	
	protected int m_killCount;
	protected int m_deathCount;
	protected int m_playerHealth;
	protected int m_shotsFired;

	MobBoss m_mobBoss;
	Bastet m_bastet;

	public Font m_scoreFont = null;
	public int m_scoreXOffset = 0;
	public int m_scoreYOffset = 0;
//	public int m_scoreHeight = 100;
	protected float m_scoreHeight { get { return ((ScoreStyle.CalcSize(new GUIContent(statsString)).y)); } }
	protected float m_scoreWidth { get { return ((ScoreStyle.CalcSize(new GUIContent(statsString)).x)); } }
	protected float m_remainingEnemiesHeight { get { return ((RemainingEnemiesStyle.CalcSize(new GUIContent(remainingEnemiesString)).y)); } } 
	protected float m_remainingEnemiesWidth { get { return ((RemainingEnemiesStyle.CalcSize(new GUIContent(remainingEnemiesString)).x)); } } 
//	public int m_scoreWidth = 100;
	public Color m_scoreRectColor = new Color(0, 0, 0, 0.5f);

	protected float m_timerHeight { get { return ((WaveLabelStyle.CalcSize(new GUIContent(waveCounterLabel)).y + WaveTimerStyle.CalcSize(new GUIContent(secondsToNextWave)).y)); } }
	protected float m_timerWidth { get { return ((WaveLabelStyle.CalcSize(new GUIContent(waveCounterLabel)).x)); } }

	public Texture Background = null;

	protected Rect m_scoreRectangle { get { return new Rect(m_scoreXOffset, m_scoreYOffset, 40 + m_scoreWidth, 20 + m_scoreHeight); } }
	protected Rect m_remainingEnemiesRectangle { get { return new Rect(Screen.width - m_remainingEnemiesWidth, 0, m_remainingEnemiesWidth, m_remainingEnemiesHeight); } }
	protected Rect m_timerRectangle { get { return new Rect((Screen.width - m_timerWidth) / 2, (Screen.height - m_timerHeight) / 2, m_timerWidth, m_timerHeight); } } 

	protected GUIStyle m_ScoreStyle = null;
	protected GUIStyle m_RemainingEnemiesStyle = null;
	protected GUIStyle m_WaveTimerStyle = null;
	protected GUIStyle m_WaveLabelStyle = null;
	public GUIStyle ScoreStyle
	{
		get
		{
			if (m_ScoreStyle == null)
			{
				m_ScoreStyle = new GUIStyle("Label");
				m_ScoreStyle.font = m_scoreFont;
				m_ScoreStyle.alignment = TextAnchor.MiddleLeft;
				m_ScoreStyle.fontSize = 16;
				m_ScoreStyle.wordWrap = false;
			}
			return m_ScoreStyle;
		}
	}

	public GUIStyle RemainingEnemiesStyle
	{
		get
		{
			if (m_RemainingEnemiesStyle == null)
			{
				m_RemainingEnemiesStyle = new GUIStyle("Label");
				m_RemainingEnemiesStyle.font = m_scoreFont;
				m_RemainingEnemiesStyle.alignment = TextAnchor.MiddleRight;
				m_RemainingEnemiesStyle.fontSize = 16;
				m_RemainingEnemiesStyle.wordWrap = false;
			}
			return m_RemainingEnemiesStyle;
		}
	}

	public GUIStyle WaveTimerStyle
	{
		get
		{
			if (m_WaveTimerStyle == null)
			{
				m_WaveTimerStyle = new GUIStyle("Label");
				m_WaveTimerStyle.font = m_scoreFont;
				m_WaveTimerStyle.alignment = TextAnchor.LowerCenter;
				m_WaveTimerStyle.fontSize = 48;
				m_WaveTimerStyle.wordWrap = false;
			}
			return m_WaveTimerStyle;
		}
	}

	public GUIStyle WaveLabelStyle
	{
		get
		{
			if (m_WaveLabelStyle == null)
			{
				m_WaveLabelStyle = new GUIStyle("Label");
				m_WaveLabelStyle.font = m_scoreFont;
				m_WaveLabelStyle.alignment = TextAnchor.UpperCenter;
				m_WaveLabelStyle.fontSize = 28;
				m_WaveLabelStyle.wordWrap = false;
			}
			return m_WaveLabelStyle;
		}
	}

	// Use this for initialization
	void Start () {
		m_killCount = 0;
		m_playerHealth = 100;

		m_mobBoss = GameObject.Find("MobBoss").GetComponent<MobBoss>();
		if (!m_mobBoss)
			Debug.LogError("StatsScript could not find 'MobBoss' script!");
		m_bastet = GameObject.Find("Bastet").GetComponent<Bastet>();
		if (!m_bastet)
			Debug.LogError("StatsScript could not find 'Bastet' script!");
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void AddKill() {
		m_killCount += 1;
		Debug.Log("Kill Count = " + m_killCount);
	}

	public void AddDeath()
	{
		m_deathCount += 1;
		m_bastet.LoseLife();
		Debug.Log("Death Count = " + m_deathCount);
	}

	public void TakeDamage(int damage) {
		m_playerHealth -= damage;
	}

	public float GetKDRatio()
	{
		int kills = m_killCount;
		int deaths = m_deathCount;

		if (kills < 1)
		{
			kills = 1;
		}

		if (deaths < 1)
		{
			deaths = 1;
		}

		return ((float)kills)/((float)deaths);
	}

	void OnGUI () {
//		GUI.color = m_scoreRectColor;
//		GUI.Box(m_scoreRectangle, "");
		GUI.color = m_scoreRectColor;
		GUI.DrawTexture(m_scoreRectangle, Background);
		GUI.DrawTexture(m_remainingEnemiesRectangle, Background);

		GUI.color = Color.white;
		GUI.Label(m_scoreRectangle, statsString, ScoreStyle);
		GUI.Label(m_remainingEnemiesRectangle, remainingEnemiesString, RemainingEnemiesStyle);

		if (m_mobBoss.MobBossState == MobBoss.StateFSM.WarmingUp)
		{
			GUI.color = m_scoreRectColor;
			GUI.DrawTexture(m_timerRectangle, Background);
			GUI.color = Color.white;
			GUI.Label(m_timerRectangle, waveCounterLabel, WaveLabelStyle);
			GUI.Label(m_timerRectangle, secondsToNextWave, WaveTimerStyle);
		}
	}

	string statsString
	{
		get
		{
			string sString = "Lives Remaining: " + (m_bastet.GetLives() - 1) + "\n"
				+ "Kills: " + m_killCount + "\n"
				+ "Deaths: " + m_deathCount + "\n"
					+ "Shots Fired: " + shotsFired + "\n"
					+ "K/D Ratio: " + GetKDRatio().ToString("F2");

			return sString;
		}
	}

	public int shotsFired
	{
		get
		{
			return m_shotsFired;
		}

		set
		{
			m_shotsFired = value;
		}
	}

	string secondsToNextWave
	{
		get
		{
			string seconds = m_mobBoss.GetSecondsToNextWave().ToString();
			return seconds;
		}
	}

	string remainingEnemiesString
	{
		get
		{
			string remainingEnemiesString = "Enemies Remaining: " + m_mobBoss.GetActiveEnemyCount();
			return remainingEnemiesString;
		}
	}

	string waveCounterLabel = "Seconds To Next Wave";
}
