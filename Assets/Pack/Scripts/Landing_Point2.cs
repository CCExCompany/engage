using UnityEngine;
using System.Collections;
//This script is for landing, and to take off after time
public class Landing_Point2 : MonoBehaviour {
	public Transform target;
	public Transform take_off;//Take_off point
	private int time;
	private int timeaux;
	private bool act;
	// Use this for initialization
	// Use this for initialization
	void Start () {
		timeaux=0;
		act=false;
		time=400; //You must change the event for a click or you want
	}
	
	// Update is called once per frame
	
	void Update() {
			float step = 0.5f * Time.deltaTime;
			timeaux++;
			if(timeaux==time){
				act=!act;
				timeaux=0;
			}
			if(act==true){
				transform.position = Vector3.MoveTowards(transform.position, take_off.position, step);
			}else{
				transform.position = Vector3.MoveTowards(transform.position, target.position, step);
			}
	}
}
