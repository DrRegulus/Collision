//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34011
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;



public class DialogueCollider : MonoBehaviour {
	
	public GameObject thePrefab;
	private GameObject instance;
	//public GameObject player; 
	//public string NPC = "bob";
	public int speechNumbers = 3;
	private int currentSpeech = 0;
	public bool activated = false;
	public bool talkedTo = false;
	
	void Update()
	{
		//Debug.Log ("hello");
		if(activated && Input.GetKeyUp(KeyCode.E) && talkedTo == false)
		{
			currentSpeech++;
			
			if(currentSpeech == speechNumbers){
				Destroy (instance);
				currentSpeech = 0;
				//currentSpeech = rnd;
				activated = false;
				talkedTo = true;
			}
			else{
				//Debug.Log (currentSpeech);
				instance.SendMessage ("TextDisplay", currentSpeech);
			}
		}
		else if (instance == null && talkedTo == true){
			
		}
		else if(talkedTo == true && Input.GetKeyUp (KeyCode.E)){
			currentSpeech ++;
			//instance.SendMessage("TextDisplay", currentSpeech);
			//Debug.Log ("ombgdsf");
			//int speechToSend = 2;
			//Debug.Log (speechToSend);
			if(currentSpeech != 111){
				currentSpeech = 0;
				//Debug.Log (speechToSend);
				//instance.SendMessage ("TextDisplay", speechToSend);
				Destroy (instance);
			}
			/*else{
				Debug.Log ("butts");
				Debug.Log (speechToSend);
				instance.SendMessage ("TextDisplay", speechToSend);
			}*/
			
		}
		
		
		/*else{
			instance.SendMessage("TextDisplay", currentSpeech);
			//instance.SendMessage ("TextDisplay", talkedTo);
			print ("cool");
		}*/
	}
	
	
	
	void OnTriggerEnter2D (Collider2D col)
	{
		if(col.tag == "Player")
		{
			//print (NPC);
			//print ("collision with NPC");
			//player = GameObject.Find ("Aliver");
			//player.GetComponent<AliverController> ().enabled = false;
			//player.rigidbody2D.velocity.Set (0, 0);
			/*if (Input.GetKeyDown (KeyCode.F) && spawnLimit == 0) {
				print ("Textbox Spawn");
				Vector3 temp = transform.position;
				temp.x = transform.position.x - 5;
				temp.y = transform.position.y + 6;
				UnityEngine.Quaternion temp2 = transform.rotation;
				temp2.z = transform.rotation.z - 90;
				instance = Instantiate (thePrefab, temp, temp2) as GameObject;
				spawnLimit = 1;
			}*/
			if (!activated) {
				Vector3 temp = transform.position;
				temp.x = transform.position.x + 1;
				Quaternion temp2 = transform.rotation;
				temp2.z = transform.rotation.z - 90;
				//print ("spawning prompt");
				instance = Instantiate (thePrefab, temp, temp2) as GameObject;
				instance.transform.parent = transform;
				instance.SendMessage ("TheStart", gameObject.name);
				//Debug.Log (currentSpeech);
				if(talkedTo == true){
					currentSpeech = Random.Range (0, 4);
					//Debug.Log (currentSpeech);
				}
				instance.SendMessage("TextDisplay", currentSpeech);
				activated = true;
				
			}
		}
	}
	
	void OnTriggerExit2D (Collider2D col)
	{
		if(col.tag == "Player")
		{
			//print ("Exit");
			if (activated) {
				
				//print ("dead");
				Destroy(instance);
				activated = false;
			}
		}
	}
}

