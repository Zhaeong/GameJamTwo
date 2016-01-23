using UnityEngine;
using System.Collections;


public class CountDownScript : MonoBehaviour {

	private float startTime;
	private float restSeconds;
	private int roundedRestSeconds;
	private int displaySeconds;
	private int displayMinutes;
	
	public int countDownSeconds = 10;

    private PlayerController playerController;

    // Use this for initialization
    void Start () {

    }
	
	public void Awake() {
		startTime = Time.time;
	}

	void OnGUI () {
		//make sure that your time is based on when this script was first called
		//instead of when your game started
		float guiTime = Time.time - startTime;

		restSeconds = countDownSeconds - (guiTime);
		
		//display messages or whatever here, do stuff based on your timer
		if (restSeconds == 60) {
			print ("One Minute Left");
		}
		if (restSeconds <= 0) {
			Debug.Log("Time is Over");
            playerController = gameObject.GetComponent<PlayerController>();
            playerController.RespawnPlayer();
            countDownSeconds = 10;
            Awake();
        }
		
		//display the timer
		roundedRestSeconds = Mathf.CeilToInt(restSeconds);
		displaySeconds = roundedRestSeconds % 60;
		displayMinutes = roundedRestSeconds / 60; 
		string text = string.Format("{0:00}:{1:00}", displayMinutes, displaySeconds); 
		GUI.Label (new Rect(400, 25, 100, 30), text);
	}
	
	// Update is called once per frame
	void Update () {
        /*
        if (restSeconds <= 0)
        {
            Debug.Log("Time is Over");
            playerController = gameObject.GetComponent<PlayerController>();
            playerController.RespawnPlayer();
            Start();


        }*/
    }
}
