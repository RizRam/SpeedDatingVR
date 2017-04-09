using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//State of the game
public class GameController : MonoBehaviour {

    static int TOTAL_ROOMS = 3;

    GameObject player;

    public float datingDurationInSeconds;  //time of dates
    public float shiftDistance;  //distance of room shift
    public double shiftTime;  //time it takes to shift

    float currentTime;
    float dateTimeEnd;  //end of dating time

    Vector3 startPosition;
    Vector3 endPosition;
    bool arrived;  //if player has finished transitioning to new room

    GameObject rooms;  //reference to rooms parent object

    GameObject feedback;
    //public GameObject endRoom;
    //Vector3 endRoomStart;
    float xStart;

    //int count;

	// Use this for initialization
	void Start ()
    {
        feedback = GameObject.FindGameObjectWithTag("Feedback");

        player = GameObject.FindGameObjectWithTag("Player");

        rooms = GameObject.FindGameObjectWithTag("Rooms");

        xStart = rooms.transform.position.x;

        //set the start and end position
        startPosition = endPosition = rooms.transform.position;
        arrived = true;

        currentTime = Time.time;
        dateTimeEnd = currentTime + datingDurationInSeconds;

        resetFeedBack();

        //count = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //update time
        currentTime += Time.deltaTime;
        //Debug.Log(rooms.transform.position);

        //if dating time is up
        if (dateTimeIsUp && arrived)
        {
            //Debug.Log("Time Up!");
            MoveToNextRoom();
        }

        if (!arrived)
        {
            Warp();
        }
    }

    void MoveToNextRoom()
    {
        Debug.Log(transform.position.x + 200);
        Debug.Log(xStart);
        if (rooms.transform.position.x + 200 == xStart) return;


        resetFeedBack();

        //Debug.Log("Move");
        endPosition = new Vector3(startPosition.x - shiftDistance, startPosition.y, startPosition.z);
        //Debug.Log(startPosition == endPosition);
        //Debug.Log(endPosition);
        arrived = false;

        AudioSource swoosh = player.GetComponent<AudioSource>();
        swoosh.Play();

        //count++;
    }

    void Warp()
    {
        //Debug.Log("Warp");
        //loat curX = rooms.transform.position.x - (Speed * Time.deltaTime);

        //rooms.transform.position = new Vector3(curX, rooms.transform.position.y, rooms.transform.position.z);

        rooms.transform.position = Vector3.MoveTowards(rooms.transform.position, endPosition, Speed * Time.deltaTime);


        //arrived
        if (Vector3.Distance(rooms.transform.position, endPosition) < 0.1f)
        //if (rooms.transform.position == endPosition)
        {
            //Debug.Log("STOP!");
            arrived = true;
            startNewDate();
        }
    }

    public void LoveButtonPress()
    {
        //Debug.Log("LoveButtonWasPressed");
        ParticleSystem ps = feedback.GetComponent<ParticleSystem>();
        ps.Play();        
    }

    public void HateButtonPress()
    {
        MoveToNextRoom();
    }

    void resetFeedBack()
    {
        ParticleSystem ps = feedback.GetComponent<ParticleSystem>();
        ps.Stop();
    }

    void startNewDate()
    {
        dateTimeEnd = currentTime + datingDurationInSeconds;
        startPosition = rooms.transform.position;
    }

    //When date is over
    bool dateTimeIsUp
    {
        get
        {
            return currentTime >= dateTimeEnd;
        }
    }
  
    //Speed of transition
    float Speed { get { return shiftDistance / (float)shiftTime; } }

}
