using UnityEngine;
using System.Collections;

public class StatsScript : MonoBehaviour {
	
	protected int m_killCount;
	protected int m_deathCount;
	protected int m_playerHealth;

	public Font m_scoreFont = null;
	public int m_scoreXOffset = 0;
	public int m_scoreYOffset = 0;
//	public int m_scoreHeight = 100;
	public float m_scoreHeight { get { return ((ScoreStyle.CalcSize(new GUIContent(statsString)).y)); } }
	protected float m_scoreWidth { get { return ((ScoreStyle.CalcSize(new GUIContent(statsString)).x)); } }
//	public int m_scoreWidth = 100;
	public Color m_scoreRectColor = new Color(0, 0, 0, 0.5f);

	public Texture Background = null;

	protected Rect m_scoreRectangle { get { return new Rect(m_scoreXOffset, m_scoreYOffset, 40 + m_scoreWidth, 20 + m_scoreHeight); } }

	protected GUIStyle m_ScoreStyle = null;
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

	// Use this for initialization
	void Start () {
		m_killCount = 0;
		m_playerHealth = 100;
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
}
