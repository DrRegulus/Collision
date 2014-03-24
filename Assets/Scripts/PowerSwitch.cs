using UnityEngine;
using System.Collections;
using System;
using System.Diagnostics;

public class PowerSwitch : MonoBehaviour {
	
	public int secondsDelayed = 3, delayElapsed, checkDelayElapsed, deactivateElapsed, checkDeactivateElapsed;
	public bool doneDelay, checkState, checkDoneDelay;
	public Stopwatch delay = new Stopwatch(), deactivate = new Stopwatch();



	public Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();

		//set up event handlers for CheckPoints and Resets
		AliverController aliver = GameObject.Find("Aliver1").GetComponent<AliverController>();
		aliver.CheckPoint += new AliverController.CheckPointEventHandler(CheckPointReached);
		aliver.Reset += new AliverController.ResetEventHandler(ResetToCheckPoint);

		delay = new Stopwatch();
		deactivate = new Stopwatch();

		delayElapsed = 0;
		deactivateElapsed = 0;
		checkDelayElapsed = 0;
		checkDeactivateElapsed = 0;
		doneDelay = false;

	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag != "weapon"){
		if(!anim.GetBool("Power")){
			
			delay.Stop();
			delay.Reset();
			delay.Start();
			doneDelay = false;

			delayElapsed = 0;
		}
		
		anim.SetBool ("Power", true);
		}
	}

	void FixedUpdate()
	{
		
		if( (delay.ElapsedMilliseconds + delayElapsed) > (1000 * secondsDelayed) ){

			if(!doneDelay){

				transform.parent.GetComponent<Maglev> ().Activate ();

				deactivate.Stop();
				deactivate.Reset();
				deactivate.Start();

				doneDelay = true;

				deactivateElapsed = 0;
			}

			if( (deactivate.ElapsedMilliseconds + deactivateElapsed) > 5000){

				anim.SetBool ("Power", false);
				delayElapsed = 0;
				delay.Stop();
				delay.Reset();

				deactivateElapsed = 0;
				deactivate.Stop();
				deactivate.Reset();
			}

		}
		
	}

	public void CheckPointReached(object sender, EventArgs e){

		checkState = anim.GetBool("Power");
		
		delayElapsed += (int) delay.ElapsedMilliseconds;
		checkDelayElapsed = delayElapsed;

		deactivateElapsed += (int) deactivate.ElapsedMilliseconds;
		checkDeactivateElapsed = deactivateElapsed;

		delay.Stop();
		delay.Reset();

		if(checkState){

			delay.Start();
		}

		deactivate.Stop();
		deactivate.Reset();
		deactivate.Start();

		checkDoneDelay = doneDelay;
	}
	
	public void ResetToCheckPoint(object sender, EventArgs e){
		
		anim.SetBool ("Power", checkState);
		
		delayElapsed = checkDelayElapsed;
		deactivateElapsed = checkDeactivateElapsed;

		delay.Stop();
		delay.Reset();

		if(checkState){
			
			delay.Start();
		}
		
		deactivate.Stop();
		deactivate.Reset();
		deactivate.Start();

		doneDelay = checkDoneDelay;
	}

}
