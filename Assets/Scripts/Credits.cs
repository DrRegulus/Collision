using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	private float speed = 0.05f;
	private bool crawling = true;
	private AudioSource music;

	// Use this for initialization
	void Start () {
		music = GetComponentInChildren<AudioSource> ();

		// init text here, more space to work than in the Inspector (but you could do that instead)
		GUIText tc = GetComponent<GUIText>();
		string creds = "COLLISION\n\n\n";
		creds += "LEAD PRODUCER\nZsade Fleming\n\n\n";
		creds += "ARTWORK & STORY\nZsade Fleming\n\n\n";
		creds += "ANIMATION & LEVEL DESIGN\nLogan Adams\n\n\n";
		creds += "PROGRAMMERS\n";
		creds += "Logan Adams\n";
		creds += "Yohan Kim\n";
		creds += "Tyler Larkin\n";
		creds += "Brendan Plante\n";
		creds += "Ryan Wnuk-Fink\n";
		creds += "\n\n\n";
		creds += "MUSIC\n\n";
		creds += "Opening Menu\n\"Word play\" by Wendy Mao\n\n";
		creds += "Intro\n\"Rend\" by Ziyed Hedfi\n\n";
		creds += "Comic 1\n\"Rend\" by Ziyed Hedfi\n\n";
		creds += "Level 1\n\"Air\" by Ziyed Hedfi\n\n";
		creds += "Comic 2\n\"Rend\" by Ziyed Hedfi\n\n";
		creds += "Level 2\n\"Walking\" by Ziyed Hedfi\n\n";
		creds += "End Credits\n\"Word Play\" by Wendy Mao\n\n";
		tc.text = creds;
		tc.transform.position = new Vector3 (0.5f, -1, 0);
		crawling = true;
	}
	
	// Update is called once per frame
	void Update () {
		print (music.time);

		if (crawling)
		{
			transform.Translate(Vector3.up * Time.deltaTime * speed);
			if (gameObject.transform.position.y > 2)
			{
				crawling = false;
			}
		}
		else if(!music.isPlaying || Input.GetKey(KeyCode.Escape))
		{
			crawling = false;
			Application.LoadLevel("MainMenu");
		}
	}
}
