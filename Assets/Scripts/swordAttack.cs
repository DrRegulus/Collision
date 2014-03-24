using UnityEngine;
using System.Collections;

public class swordAttack : MonoBehaviour {
	public Sprite sword;
	static private BoxCollider2D boxCol;// = (BoxCollider2D)collider2D;
	static private SpriteRenderer sprRenderer;// = (SpriteRenderer)renderer;
//	private AliverController playerCtrl;
		void Awake()
	{
			sprRenderer = (SpriteRenderer)renderer;
			boxCol = (BoxCollider2D)collider2D;
			sprRenderer.sprite = null;	
			boxCol.size = new Vector2(sword.bounds.size.x, sword.bounds.size.y);
			boxCol.enabled = false;
		//	playerCtrl = transform.root.GetComponent<AliverController>();

	}

	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Z))
		{
			sprRenderer = (SpriteRenderer)renderer;
			boxCol = (BoxCollider2D)collider2D;
			sprRenderer.sprite = sword;
			boxCol.enabled = true;
		}
		if (Input.GetKeyUp (KeyCode.Z)) {
			back();	
		}

	}
	void back()
	{
		if (Input.GetKeyUp (KeyCode.Z)) {
			sprRenderer = (SpriteRenderer)renderer;
			boxCol = (BoxCollider2D)collider2D;
			sprRenderer.sprite = null;
			boxCol.enabled = false;		
		}
	}
	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.tag == "Enemy") {
			col.gameObject.GetComponent<Enemy> ().Hurt ();
		}
	}

}
