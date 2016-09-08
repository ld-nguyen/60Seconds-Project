
/*
 * Wipe - Laura Eckhardt 80% - Long Duc Nguyen 20%
 * 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wipe : MonoBehaviour {

    private Vector3 touchPositionDown;
    private int number;
	private int maxNumber; // Für setFXAlpha Methode
	[SerializeField]
	private bool useMuscleFadeOut; //Nur bei MuscleAche benutzen
	[SerializeField]
	private bool useFadeOut;
	[SerializeField]
	private bool useSound;
	private SpriteRenderer[] sp;
	private AudioSource audio;
	public AudioClip[] audioClips;

    // random number between 4 and 7: how many time the player has to wipe
	void Start () {
        //randomNumber();
		maxNumber = number;
		if (useMuscleFadeOut) { //Find SpriteRenderers
			sp = this.gameObject.GetComponentsInChildren<SpriteRenderer> ();
		} else if (useFadeOut) {
			sp = this.gameObject.GetComponents<SpriteRenderer>();
		}
		if (useSound) {
			audio = this.gameObject.GetComponent<AudioSource>();
		}
	}

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0));
            if (collider2D.bounds.IntersectRay(ray))
            {
                // remembers the mouse position, when touched
                if (touch.phase == TouchPhase.Began)
                {
                    touchPositionDown = Input.mousePosition;
                }
            }

        }
    }

    void OnMouseEnter()
    {
        touchPositionDown = Input.mousePosition;
    }

    // checks if the mouse position is not the same as touchPositionDown.
    // if yes: number of needed wipes is reduced
    // if number == 0: illness is cured
    void OnMouseExit()
    {
        Vector3 touchPositionUp = Input.mousePosition;
        number--;
        touchPositionDown = touchPositionUp;
		if (useSound) {
			audio.clip = audioClips[Random.Range(0,audioClips.Length)];
			audio.Play();
		}
        if (useMuscleFadeOut || useFadeOut)
        {
            setFxAlpha(maxNumber, number);
        }

        

        if (number == 0)
        {
            gameObject.SetActive(false);
			setFxAlpha(1, 1);

			SendMessageUpwards("finishWorkStep", new MsgObject(gameObject.name, TaskConsts.WIPE));
            
            AnimationFunctions.diseaseSolvedFace();
            AnimationFunctions.diseaseSolvedFX(this.gameObject.transform.position);
            
        }
    }

	public int getNumber(){
		return number;
	}
    // random number between 4 and 7
    private void randomNumber()
    {
        number = Random.Range(4, 8);
        Debug.Log("wipe: " + number);
    }

	public void setFxAlpha(int maxNumber, int number){
		foreach (SpriteRenderer s in sp) {
			Color newAlpha = new Color (s.color.r, s.color.g, s.color.b, ((float)number / (float)maxNumber));
			//Debug.Log (newAlpha);
			//Debug.Log (sp.color);
			s.color = newAlpha;
		}
	}

    public void setEffort(int times)
    {
        this.number = times;
    }
}
