/*
 * TipOver - Laura Eckhardt 85% Long Duc Nguyen 15 %
 */

using UnityEngine;
using System.Collections;

public class TipOver : MonoBehaviour {

    public enum Side
    {
        right,
        left,
        backward,
        upsideDown
    }

    //Now we can make a variable using that set as its type!
    public Side side;
    private float time = 2;

	public bool hasDrops; //Bei Nasenbluten, Wasser im Ohr o.ä benutzen!
	private float origTimer;
	private InitiateDrops drop;

    private float startTime;
    private bool startTimeSet;

	// Use this for initialization
	void Start () {
        startTimeSet = false;
		if (hasDrops) {
			drop = this.gameObject.GetComponent<InitiateDrops>();
			origTimer = drop.pauseTimer;
		}
	}
	
	// Update is called once per frame
	void Update () {
        switch(side){
            case Side.left:
                if (Input.acceleration.x < -0.5)
                {
                    if (!startTimeSet)
                    {
                        startTimeSet = true;
                        startTime = Time.time;
						Handheld.Vibrate();
                    }
                    else if ((Time.time - startTime) >= time)
                    {
						gameObject.SetActive(false);

						SendMessageUpwards("finishWorkStep", new MsgObject(gameObject.name, TaskConsts.TIP_OVER));
						
						AnimationFunctions.diseaseSolvedFX(this.gameObject.transform.position);
						AnimationFunctions.diseaseSolvedFace();
						
						Debug.Log("links");

                    }
                }
                else
                {
                    startTimeSet = false;
                }
				if(hasDrops){
					drop.pauseTimer = origTimer+3*(Input.acceleration.x); //Increase Drop Rate by adding the negative x acceleration
					if(drop.pauseTimer <= 0.5f) drop.pauseTimer = 0.5f;
				}
                break;
            case Side.right:
                if (Input.acceleration.x > 0.5){
                    if (!startTimeSet)
                    {
                        startTimeSet = true;
                        startTime = Time.time;
						Handheld.Vibrate();
                    }
                    else if ((Time.time - startTime) >= time)
                    {
						gameObject.SetActive(false);

						SendMessageUpwards("finishWorkStep", new MsgObject(gameObject.name, TaskConsts.TIP_OVER));

						AnimationFunctions.diseaseSolvedFX(this.gameObject.transform.position);
						AnimationFunctions.diseaseSolvedFace();
						
                        Debug.Log("rechts");
						
                    }
                }
                else
                {
                    startTimeSet = false;
                }
				if(hasDrops){
					drop.pauseTimer = origTimer-3*(Input.acceleration.x); //Increase Drop Rate by substracting the positive x acceleration
					if(drop.pauseTimer <= 0.5f) drop.pauseTimer = 0.5f;
				}
                break;
            case Side.backward:
                if(Input.acceleration.y > 0)
                {
                    if (!startTimeSet)
                    {
                        startTimeSet = true;
                        startTime = Time.time;
						Handheld.Vibrate();
                    }
                    else if ((Time.time - startTime) >= time)
                    {

						gameObject.SetActive(false);

						SendMessageUpwards("finishWorkStep", new MsgObject(gameObject.name, TaskConsts.TIP_OVER));
						
						AnimationFunctions.diseaseSolvedFX(this.gameObject.transform.position);
						AnimationFunctions.diseaseSolvedFace();
						
                        Debug.Log("liegt");
						
                    }
                }
                else
                {
                    startTimeSet = false;
                }
				if(hasDrops){
				drop.pauseTimer = origTimer-3*(Input.acceleration.y); 
				if(drop.pauseTimer <= 0.5f) drop.pauseTimer = 0.5f;
				}
                break;
            case Side.upsideDown:
                if ((0.9 <= Input.acceleration.y) && (Input.acceleration.y <= 1.1))
                {
                    if (!startTimeSet)
                    {
                        startTimeSet = true;
                        startTime = Time.time;
						Handheld.Vibrate();
                    }
                    else if ((Time.time - startTime) >= time)
                    {
						gameObject.SetActive(false);

						SendMessageUpwards("finishWorkStep", new MsgObject(gameObject.name, TaskConsts.TIP_OVER));
						
						AnimationFunctions.diseaseSolvedFX(this.gameObject.transform.position);    
						AnimationFunctions.diseaseSolvedFace();
						
                        Debug.Log("kopf");
						
                    }
                }
                else
                {
                    startTimeSet = false;
                }
				if(hasDrops){
				drop.pauseTimer = origTimer+(Input.acceleration.y); //Decrease the Droprate by adding the positive y acceleration
					if(drop.pauseTimer <= 0.8f) drop.pauseTimer = 0.8f;
				}
                break;
        }
	}

    public void setEffort(float time)
    {
        this.time = time;
    }
}
