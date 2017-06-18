using System.Collections;
using System.Collections.Generic;
using Assets.scripts;
using UnityEngine;

public class ChangeBackground : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		var manager = GameObject.FindGameObjectWithTag("StateManager").GetComponent<StateManagerContainer>();
			
		var canvas = GameObject.FindGameObjectWithTag("Canvas");
		if (canvas == null) {
			return;
		}
		
		foreach (Transform transform in canvas.transform) {                       
			if (transform.name.Equals(manager.manager.Player1.CurrentLocation.Name)) {    
                                                                          
				transform.gameObject.SetActive(true);                             
				Debug.Log("Dimmerminner");                                        
				break;                                                            
			}                                                                     
		}                                                                         
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
