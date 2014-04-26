using UnityEngine;
using System.Collections;

public class EndTutorial : MonoBehaviour {

	public Transform atticus;
	public Transform steamVent;

	private DialogueCollider atticusDialogue;
	private SteamValve steam;

	private bool unlocked = false;

	void Start()
	{
		atticusDialogue = atticus.GetComponent<DialogueCollider> ();
		steam = steamVent.GetComponentInChildren<SteamValve> ();
	}

	// Update is called once per frame
	void Update () {
		if(!unlocked && atticusDialogue.activated)
		{
			steam.debug = true;
			unlocked = true;
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			AliverController aliver = col.transform.GetComponent<AliverController>();
			aliver.Freeze();
			PlayerPrefs.SetInt("Lives", aliver.lives);
			Application.LoadLevel(Application.loadedLevel + 1);
		}
	}
}
