using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour {
    public enum Buttons { Love, Hate }


    public Buttons button;
    public GameObject gameController;


    GameController controller;

    //Collider collider;

	// Use this for initialization
	void Start () {
        controller = gameController.GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("hand"))
        {
            //Debug.Log("Enter");
            switch (button)
            {
                case Buttons.Love:
                    controller.LoveButtonPress();
                    break;
                case Buttons.Hate:
                    Debug.Log("Hate Pressed");
                    controller.HateButtonPress();
                    break;

                default:
                    break;
            }
        }        
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Stay");
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision");
    }
}
