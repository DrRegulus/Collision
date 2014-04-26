using UnityEngine;
using System.Collections;
using System;
using System.Diagnostics;

public class PowerSwitch : MonoBehaviour {

	public bool debug = false;
	private bool checkState;
	public Animator anim;

	// Use this for initialization
	void Start () {
		//set up event handlers for CheckPoints and Resets
		AliverController aliver = GameObject.Find("Aliver").GetComponent<AliverController>();
		aliver.CheckPoint += new AliverController.CheckPointEventHandler(CheckPointReached);
		aliver.Reset += new AliverController.ResetEventHandler(ResetToCheckPoint);
	}

	void Update()
	{
		if(debug)
		{
			transform.parent.GetComponent<Machine>().Activate();
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Powered"){
			if(!anim.GetBool("Power")){
				anim.SetBool ("Power", true);

				transform.parent.GetComponent<ShipMaglev>().Activate();
			}
		}
	}


	public void CheckPointReached(object sender, EventArgs e){

		checkState = anim.GetBool("Power");
	}
	
	public void ResetToCheckPoint(object sender, EventArgs e){
		
		anim.SetBool ("Power", checkState);
	}

}
