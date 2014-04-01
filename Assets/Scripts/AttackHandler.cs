using UnityEngine;
using System.Collections;

public class AttackHandler : MonoBehaviour {

	private AliverController playerCtrl;		// Reference to the PlayerControl script.
	public ThrowableWeapon throwW;			// Prefab of the shootingStar
	private float lastTime = 0.0f;
	public float coolDown = 3.0f;			//able to shoot after 3 sec
	private float posX = 0.2f;

	void Start()
	{
		playerCtrl = transform.parent.GetComponent<AliverController> ();
	}

	void Update ()
	{
		float move = Input.GetAxis ("Horizontal");
		if (move > 0 && playerCtrl.facingRight) {
			Flip (posX);
			}
		if (move < 0 && !playerCtrl.facingRight) {
			Flip (-posX);
		}
		if(Input.GetKeyDown(KeyCode.Mouse1))
		{
			if(Time.time - lastTime > coolDown || lastTime == 0.0f)
			{
				shoot();
				lastTime = Time.time;
			}
		}
	}
	void shoot(){
		Instantiate (throwW, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0)));
	}

	void Flip(float n){
		gameObject.transform.position = gameObject.transform.parent.TransformPoint(n, 0.1f, 0.0f);
	}
	
}
