using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        LoveButton loveButton = GetComponent<LoveButton>();
        Vector3 position = loveButton.transform.position;

	}
}
