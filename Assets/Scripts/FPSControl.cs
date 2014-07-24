using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class FPSControl : MonoBehaviour {

	public Transform Xobject;
	public Transform Yobject;
	public GameObject CharModel;
	public GameObject StandTrigger;

	public float MouseSensitivity = 5.0f;
	public float Yrange = 60.0f;

	public float speed = 2.5f;
	public float sprintSpeed = 1.75f;
	public float crouchSpeed = 0.5f;
	public float jumpSpeed = 5.0f;

	CharacterController cc;
	float xAngle = 0;
	float yAngle = 0;
	float jumpVelocity = 0;
	CanStand cs = null;
	bool crouching = false;
	bool standing = false;
	float Hcrouch;
	float Hstand;

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
		cc = GetComponent<CharacterController> ();
		if (Xobject != null)
			xAngle = Xobject.eulerAngles.y;
		if (StandTrigger != null)
			cs = StandTrigger.GetComponent<CanStand> ();

		Hstand = transform.localScale.y;
		Hcrouch = Hstand * 0.5f;
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
			Xobject.localEulerAngles = new Vector3 (0, xAngle, 0);
		if (Yobject)
			Yobject.localEulerAngles = new Vector3 (-yAngle, 0, 0);

		float Xmov = Input.GetAxis ("Horizontal") * speed;
		float Ymov = Input.GetAxis ("Vertical") * speed;
		float sprintMov = 1 + Input.GetAxis ("Sprint") * (sprintSpeed - 1);

		if (Input.GetButton ("Crouch")) {
			crouching = true;
			standing = false;
		} else {
			if (crouching)
				standing = true;
			if (standing) {
				if (cs == null || cs.ableToStand) {
					standing = false;
					crouching = false;
				}
			}
		}

		if (crouching) {
			transform.localScale = new Vector3(1, Hcrouch, 1);
			sprintMov = crouchSpeed;
		} else {
			transform.localScale = new Vector3(1, Hstand, 1);
		}

		if (cc.isGrounded) {
			if (Input.GetButtonDown ("Jump") && !crouching)
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
