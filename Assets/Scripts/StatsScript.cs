using UnityEngine;
using System.Collections;

public class StatsScript : MonoBehaviour {

	[HideInInspector]
	public int m_killCount;
	[HideInInspector]
	public int _playerHealth;

	public int m_scoreXOffset = 0;
	public int m_scoreYOffset = 0;
	public int m_scoreHeight = 100;
	protected float m_scoreWidth { get { return ((ScoreStyle.CalcSize(new GUIContent(statsString)).x)); } }
//	public int m_scoreWidth = 100;
	public Color m_scoreRectColor = new Color(0, 0, 0, 0.5f);

	public Texture Background = null;

	protected Rect m_scoreRectangle { get { return new Rect(m_scoreXOffset, m_scoreYOffset, 80 + m_scoreWidth, m_scoreHeight); } }

	protected GUIStyle m_ScoreStyle = null;
	public GUIStyle ScoreStyle
	{
		get
		{
			if (m_ScoreStyle == null)
			{
				m_ScoreStyle = new GUIStyle("Label");
//				m_ScoreStyle.font = BigFont;
				m_ScoreStyle.alignment = TextAnchor.MiddleCenter;
				m_ScoreStyle.fontSize = 28;
				m_ScoreStyle.wordWrap = false;
			}
			return m_ScoreStyle;
		}
	}

	// Use this for initialization
	void Start () {
		m_killCount = 0;
		_playerHealth = 100;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddKill() {
		m_killCount += 1;
		Debug.Log("Kill Count = " + m_killCount);
	}

	public void TakeDamage(int damage) {
		_playerHealth -= damage;
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
			return "Kill Count: " + m_killCount;
		}
	}
}
