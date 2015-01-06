using UnityEngine;
using System.Collections;

public class BendingGrass : MonoBehaviour {

	public float bentness;
	private SkinnedMeshRenderer sRenderer;
	public float sinOffset = 0;

	private float phase;

	// Use this for initialization
	void Start () {
		GameObject myObject = transform.gameObject;
		sRenderer = myObject.GetComponent<SkinnedMeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		phase = Mathf.Sin(-Time.time + sinOffset);
		sRenderer.SetBlendShapeWeight(2, map(phase, -1, 1, 0, 50));
		transform.rotation = Quaternion.Euler(-90 - map(phase, -1, 1, 0, 45), 0, 0);
	}
	
	float map(float s, float a1, float a2, float b1, float b2)
	{
		return b1 + (s-a1)*(b2-b1)/(a2-a1);
	}
}