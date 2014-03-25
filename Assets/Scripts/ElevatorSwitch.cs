using UnityEngine;
using System.Collections;
using System;
using System.Diagnostics;

public class ElevatorSwitch : MonoBehaviour {
	
	public Animator anim;
	public Stopwatch t = new Stopwatch();
	public bool checkState;
	public int elapsed, checkElapsed;
	
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		
		AliverController aliver = GameObject.Find("Aliver1").GetComponent<AliverController>();
		aliver.CheckPoint += new AliverController.CheckPointEventHandler(CheckPointReached);
		aliver.Reset += new AliverController.ResetEventHandler(ResetToCheckPoint);
		
		t = new Stopwatch();
		elapsed = 0;
		checkElapsed = 0;
		
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "weapon" || col.tag == "Player") {
			if (!anim.GetBool ("Power")) {

					t.Stop ();
					t.Reset ();
					t.Start ();

					elapsed = 0;
			}

			anim.SetBool ("Power", true);
			transform.parent.parent.GetComponent<Elevator> ().Activate ();
		}
	}
	
	
	void FixedUpdate()
	{
		
		if( (t.ElapsedMilliseconds + elapsed) > 2000 ){
			
			anim.SetBool ("Power", false);
			elapsed = 0;
			t.Stop();
			t.Reset();
		}
		
	}
	
	
	public void CheckPointReached(object sender, EventArgs e){
		
		checkState = anim.GetBool("Power");
		
		elapsed += (int) t.ElapsedMilliseconds;
		checkElapsed = elapsed;
		
		t.Stop();
		t.Reset();
		t.Start();
		
	}
	
	public void ResetToCheckPoint(object sender, EventArgs e){
		
		anim.SetBool ("Power", checkState);
		
		elapsed = checkElapsed;
		
		t.Stop();
		t.Reset();
		t.Start();
		
	}
	
}
