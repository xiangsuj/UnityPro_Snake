using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void UpPress(){
		MainUIController.Instance.direction = 0;

	}
	public void RightPress(){
		MainUIController.Instance.direction = 1;
	}
	public void DownPress(){
		MainUIController.Instance.direction = 2;
	}
	public void LeftPress(){
		MainUIController.Instance.direction = 3;
	}
}
