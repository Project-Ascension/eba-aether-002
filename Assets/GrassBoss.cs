using UnityEngine;
using System.Collections;

public class GrassBoss : MonoBehaviour {

	public GameObject grassPrefab;
	public float margin = 1;
	public int x_dimensions = 32;
	public int z_dimensions = 32;
	public float scatter = 1.0f;

	// Use this for initialization
	void Start () {
		for (int z = 0; z < z_dimensions; z++) {
			for (int x = 0; x < x_dimensions; x++) {
				float randX = Random.Range(-scatter, scatter);
				float randZ = Random.Range(-scatter, scatter);
//				float randX = 0;
//				float randZ = 0;
				GameObject grassClone = (GameObject) Instantiate(grassPrefab, new Vector3(x * margin + randX, 0, z * margin + randZ), new Quaternion());
				grassClone.transform.rotation = Quaternion.Euler(-90, 0, 0);
				BendingGrass bg = grassClone.GetComponent<BendingGrass>();
				bg.sinOffset = x * 0.1f;
			}
		}

		GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
		plane.transform.localScale = new Vector3(x_dimensions * 0.1f * margin, 1.0f, z_dimensions * 0.1f * margin);
		plane.transform.position = new Vector3(x_dimensions * margin / 2, 0, z_dimensions * margin / 2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
