/*
 * HoldClick - 80% Laura Eckhardt 20% Long Duc Nguyen
 */
using UnityEngine;
using System.Collections;

public class HoldClick : MonoBehaviour
{
    private float clickTime;
    private float timePassed;
    private float time;

	private bool released;

	private SpriteRenderer s;
	private AudioSource a;
	public GameObject fx;

    void Start()
    {
        clickTime = 0;
        timePassed = 0;
        //fingers = 0;
		time = 3;
		s = this.gameObject.GetComponent<SpriteRenderer> ();
		a = this.gameObject.GetComponent<AudioSource> ();
    }

    void Update()
    {
        // checks if the time was long enough
        foreach (Touch touch in Input.touches)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0));
            if (collider2D.bounds.IntersectRay(ray))
            {
                if (touch.phase == TouchPhase.Began)
                {
                    //clickTime = Time.time;  // remembers the time, when clicked
					released = false;
					fx.SetActive(true);
					a.Play();

                }
                else if (touch.phase == TouchPhase.Stationary)
                {
                    //timePassed = Time.time - clickTime;
					timePassed += Time.deltaTime;
                }
				else if (touch.phase == TouchPhase.Ended){
					released = true;
				}
            }
        }
		if (released) 
		{
			if(timePassed - Time.deltaTime> 0){
				timePassed -= Time.deltaTime;
			}
			else{timePassed = 0;}
		}
		setFxAlpha(time,timePassed);
    	
		if (timePassed >= time) {
			gameObject.SetActive (false);
			s.color = new Color(s.color.r, s.color.g, s.color.b, 1f);

			SendMessageUpwards("finishWorkStep", new MsgObject(gameObject.name, TaskConsts.HOLD_CLICK));

			AnimationFunctions.diseaseSolvedFX(this.gameObject.transform.position);
			AnimationFunctions.diseaseSolvedFace();

			timePassed = 0;
		}
    }

    public void setEffort(float time)
    {
        this.time = time;
    }

	public void setFxAlpha(float maxNumber, float number){
			Color newAlpha = new Color (s.color.r, s.color.g, s.color.b, ((maxNumber-number) / maxNumber));
				s.color = newAlpha;
		}
}
