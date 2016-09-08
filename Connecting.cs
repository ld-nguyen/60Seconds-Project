/*
 * Connecting - 80 % Laura Eckhardt 20% Long Duc Nguyen
 * 
 */
using UnityEngine;
using System.Collections;

public class Connecting : MonoBehaviour {

    private Transform child0;
    private Transform child1;
	private SpriteRenderer spriteRenderer; // SpriteRenderer für den ungebrochenen Knochen, liegt im Objekt welches dieses Skript als Komponente hat
	private SpriteRenderer child0sp;
	private SpriteRenderer child1sp;

    private float clickTime;
    private float timePassed;

    private bool timeNotMemorized;
	private bool active;

	private AudioSource audio;
	private AudioClip[] boneCracks;
    // bones need to be connected
	void Start () {
        child0 = transform.GetChild(0);
        child1 = transform.GetChild(1);
        clickTime = 0;
        timePassed = 0;
        timeNotMemorized = true;

		spriteRenderer = this.gameObject.GetComponent<SpriteRenderer> ();
		child0sp = child0.GetComponent<SpriteRenderer> ();
		child1sp = child1.GetComponent<SpriteRenderer> ();
		audio = this.gameObject.GetComponent<AudioSource> ();
		boneCracks = Resources.LoadAll<AudioClip>("Sound/Disease/Fracture");
	}

    // if distance is small enough bones will be connected
	void Update () {
	    if(checkDistance())
		{
            Screen.showCursor = true;

            child0.gameObject.SetActive(false);
            child1.gameObject.SetActive(false);

            spriteRenderer.enabled = true; //Gebrochene Knochen(Children) unsichtbar und Gesunder Knochen(Parent) sichtbar

			active = false;

			SendMessageUpwards("finishWorkStep", new MsgObject(gameObject.name, TaskConsts.CONNECTING));

            AnimationFunctions.diseaseSolvedFX(this.gameObject.transform.position);
			AnimationFunctions.diseaseSolvedFace();

        }
	}

    //checks the distance between both sprites
    private bool checkDistance()
    {
        if (child0.collider2D.bounds.Intersects(child1.collider2D.bounds))
            return true;
        else
            return false;
    }

}
