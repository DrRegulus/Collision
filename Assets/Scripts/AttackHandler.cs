using UnityEngine;
using System.Collections;

public class AttackHandler : MonoBehaviour {

	public Rigidbody2D throwW;				// Prefab of the shootingStar
	public float speed = 20f;				// The speed the rocket will fire at.
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
			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
			//need to work on change direction of attackhandler	
			// and when te lastTime is zero (right after player starts the game)
				// If the player is facing right...
				if(playerCtrl.facingRight && Time.time - lastTime > coolDown)
				{
					throw_(0, speed);
					lastTime = Time.time;
				//	Flip ();
				}
				else if(!playerCtrl.facingRight && Time.time - lastTime > coolDown)
				{
					throw_(180f, -speed);
					lastTime = Time.time;
			//	Flip ();
				}
			}
			
		}
		
		void throw_(float vec, float speed){
			Rigidbody2D bulletInstance = Instantiate(throwW, transform.position, Quaternion.Euler(new Vector3(0,0,vec))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2(speed, 0);
		}

	void Flip(float n){
		gameObject.transform.position = gameObject.transform.parent.TransformPoint(n, 0.1f, 0.0f);

		
	}
	
}
