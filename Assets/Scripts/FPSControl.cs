using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class FPSControl : MonoBehaviour {

	public GameObject Xobject;
	public GameObject Yobject;
	public GameObject CrouchObject;

	public float MouseSensitivity = 5.0f;
	public float Yrange = 60.0f;

	public float speed = 2.5f;
	public float sprint = 1.75f;
	public float jumpSpeed = 5.0f;

	CharacterController cc;
	float xAngle = 0;
	float yAngle = 0;
	float jumpVelocity = 0;

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
		cc = GetComponent<CharacterController> ();
		if (Xobject != null)
			xAngle = Xobject.transform.eulerAngles.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Quit") != 0) {
			//Quit the game...
			Screen.lockCursor = false;
			Application.Quit();
		}

		if (cc == null)
			return;

		xAngle += Input.GetAxis ("Mouse X") * MouseSensitivity;
		xAngle = modf (xAngle, 360);
		yAngle += Input.GetAxis ("Mouse Y") * MouseSensitivity;
		yAngle = Mathf.Clamp (yAngle, -Yrange, Yrange);

		if (Xobject)
			Xobject.transform.localEulerAngles = new Vector3 (0, xAngle, 0);
		if (Yobject)
			Yobject.transform.localEulerAngles = new Vector3 (-yAngle, 0, 0);

		float Xmov = Input.GetAxis ("Horizontal") * speed;
		float Ymov = Input.GetAxis ("Vertical") * speed;
		float sprintMov = (Input.GetAxis ("Sprint") != 0) ? sprint : 1;

		if (cc.isGrounded) {
			if (Input.GetButtonDown ("Jump"))
				jumpVelocity = jumpSpeed;
			else
				jumpVelocity = -0.01f;
		} else {
			jumpVelocity += Physics.gravity.y * Time.deltaTime;
		}

		Vector3 spd = new Vector3 (Xmov * sprintMov, jumpVelocity, Ymov * sprintMov);
		spd = transform.rotation * spd;
		cc.Move (spd * Time.deltaTime);
	}

	float modf(float a, float b) {
		while (a < 0)
			a += b;
		while (a >= b)
			a -= b;
		return a;
	}
}
