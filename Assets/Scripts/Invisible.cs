using UnityEngine;
using System.Collections;

[RequireComponent (typeof(MeshRenderer))]
public class Invisible : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<MeshRenderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
