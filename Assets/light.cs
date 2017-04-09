using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light : MonoBehaviour {

    System.Random rand = new System.Random();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        int num = rand.Next(0, 3600);
        Light light = GetComponent<Light>();

        if (num <= 10)
        {
            light.enabled = false;
        }
        else if (num >= 3500)
        {
            light.enabled = true;
        }
        
	}
}
