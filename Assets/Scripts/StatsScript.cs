using UnityEngine;
using System.Collections;

public class StatsScript : MonoBehaviour {
	
	protected int m_killCount;
	protected int m_deathCount;
	protected int m_playerHealth;

	MobBoss mobBoss;

	public Font m_scoreFont = null;
	public int m_scoreXOffset = 0;
	public int m_scoreYOffset = 0;
//	public int m_scoreHeight = 100;
	protected float m_scoreHeight { get { return ((ScoreStyle.CalcSize(new GUIContent(statsString)).y)); } }
	protected float m_scoreWidth { get { return ((ScoreStyle.CalcSize(new GUIContent(statsString)).x)); } }
//	public int m_scoreWidth = 100;
	public Color m_scoreRectColor = new Color(0, 0, 0, 0.5f);

	protected float m_timerHeight { get { return ((WaveLabelStyle.CalcSize(new GUIContent(waveCounterLabel)).y + WaveTimerStyle.CalcSize(new GUIContent(secondsToNextWave)).y)); } }
	protected float m_timerWidth { get { return ((WaveLabelStyle.CalcSize(new GUIContent(waveCounterLabel)).x)); } }

	public Texture Background = null;

	protected Rect m_scoreRectangle { get { return new Rect(m_scoreXOffset, m_scoreYOffset, 40 + m_scoreWidth, 20 + m_scoreHeight); } }
	protected Rect m_timerRectangle { get { return new Rect((Screen.width - m_timerWidth) / 2, (Screen.height - m_timerHeight) / 2, m_timerWidth, m_timerHeight); } } 

	protected GUIStyle m_ScoreStyle = null;
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
				m_ScoreStyle.fontSize = 28;
				m_ScoreStyle.wordWrap = false;
			}
			return m_ScoreStyle;
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

		mobBoss = GameObject.Find("MobBoss").GetComponent<MobBoss>();
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
		Debug.Log("Death Count = " + m_deathCount);
	}

	public void TakeDamage(int damage) {
		m_playerHealth -= damage;
	}

	void OnGUI () {
//		GUI.color = m_scoreRectColor;
//		GUI.Box(m_scoreRectangle, "");
		GUI.color = m_scoreRectColor;
		GUI.DrawTexture(m_scoreRectangle, Background);

		GUI.color = Color.white;
		GUI.Label(m_scoreRectangle, statsString, ScoreStyle);

		if (mobBoss.MobBossState == MobBoss.StateFSM.WarmingUp)
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
			string sString = "Kills: " + m_killCount + "\n"
				+ "Deaths: " + m_deathCount;

			return sString;
		}
	}

	string secondsToNextWave
	{
		get
		{
			string seconds = mobBoss.GetSecondsToNextWave().ToString();
			return seconds;
		}
	}

	string waveCounterLabel = "Seconds To Next Wave";
}
