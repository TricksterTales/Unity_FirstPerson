using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Collider))]
public class CanStand : MonoBehaviour {

	public Collider player;
	public bool ableToStand = true;

	private int lvlLayer = 0;
	private bool tryToStand = false;

	// Use this for initialization
	void Start () {
		Physics.IgnoreCollision (collider, player, true);
		ableToStand = true;
		lvlLayer = LayerMask.NameToLayer ("Level");
	}

	void LateUpdate() {
		if (tryToStand) {
			ableToStand = true;
			tryToStand = false;
		}
	}
	
	void OnTriggerStay(Collider collision) {
		if (collision.gameObject.layer == lvlLayer) {
			ableToStand = false;
			tryToStand = false;
		}
	}

	void OnTriggerExit() {
		tryToStand = true;
	}
}
