using UnityEngine;
using System.Collections;

public class AttackHandler : MonoBehaviour {

	public throwableWeapon throwW;				// Prefab of the shootingStar
	private float lastTime = 0.0f;
	public float coolDown = 3.0f;		//able to shoot after 3 sec
	private AliverController playerCtrl;		// Reference to the PlayerControl script.
	private float posX = 0.2f;

	void Awake()
	{
		playerCtrl = transform.root.GetComponent<AliverController>();
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
				throw_();
				lastTime = Time.time;
			}
		}
	}
	void throw_(){
		Instantiate (throwW, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0)));
	}

	void Flip(float n){
		gameObject.transform.position = gameObject.transform.parent.TransformPoint(n, 0.1f, 0.0f);
	}
	
}
