using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class SwordAttack : MonoBehaviour {
	public Sprite sword;
	public float animDuration = 0.2f;
	static private BoxCollider2D boxCol;// = (BoxCollider2D)collider2D;
	static private SpriteRenderer sprRenderer;// = (SpriteRenderer)renderer;
	private Stopwatch timer = new Stopwatch();
	private AliverController aliver;

	void Start()
	{
		aliver = GameObject.Find("Aliver").GetComponent<AliverController>();
	}

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

		if(Input.GetKeyDown(KeyCode.Mouse0) && Time.timeScale == 1)
		{
			sprRenderer = (SpriteRenderer)renderer;
			boxCol = (BoxCollider2D)collider2D;
			if(aliver.rigidbody2D.velocity.x > 0)
			{
				boxCol.transform.position.Set(aliver.transform.position.x + 1, aliver.transform.position.y, aliver.transform.position.z);
     		}
			else{
				boxCol.transform.position.Set(aliver.transform.position.x - 1, aliver.transform.position.y, aliver.transform.position.z);
			}
			sprRenderer.sprite = sword;
			boxCol.enabled = true;

			if(!timer.IsRunning)
			{
				timer.Start();
			}
		}

		if(timer.IsRunning && timer.ElapsedMilliseconds > 1000 * animDuration)
		{
			timer.Stop();
			timer.Reset();
			back ();
		}
	}

	void back()
	{
		sprRenderer = (SpriteRenderer)renderer;
		boxCol = (BoxCollider2D)collider2D;
		sprRenderer.sprite = null;
		boxCol.enabled = false;		
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.tag == "Enemy") {
			col.gameObject.GetComponent<Enemy> ().Hurt (1);
		}
	}

}
