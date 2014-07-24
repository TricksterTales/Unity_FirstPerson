using UnityEngine;
using System.Collections;

public class SetColor : MonoBehaviour {

	public Color col;

	// Use this for initialization
	void Start () {
		renderer.material.color = col;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
