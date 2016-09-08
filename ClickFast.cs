using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickFast : MonoBehaviour {

    private int clickTimes = 10;
    [SerializeField]
    private bool organ = true;
	[SerializeField]
	private bool tooth = false;
   	private int clicked;

	private GameObject fart;
	private GameObject acid;

	void Start () {
        clicked = 0;
		if (organ) {
			fart = GameObject.Find ("fart");
			acid = this.transform.GetChild (2).gameObject;
		} else if (tooth) {
			//AnimationFunctions.cariesMouth(true);
		}
	}
	
	// checks if the player clicked enough times
	void Update () {
        foreach (Touch touch in Input.touches)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0));
            if (renderer.bounds.IntersectRay(ray))
            {
                if(touch.phase == TouchPhase.Began)
                    if (clicked != clickTimes)
                        clicked++;
            }
        }

        if (clicked >= clickTimes) {
            // resets sprite, collider and script
            World world = World.getInstance();

            if (organ) {
                //SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
                //renderer.sprite = Resources.Load("Sprites/Organs/Heart", typeof(Sprite)) as Sprite; // heart
                Destroy(gameObject.GetComponent<ClickFast>());
				fart.GetComponent<Animation>().enabled = false;
				fart.GetComponent<SpriteRenderer>().enabled = false;
				acid.SetActive(false);
				world.DiseaseSystem.displayGoodGameObject(this.gameObject);
            }
			else if(tooth){ AnimationFunctions.cariesMouth(false);}

			gameObject.SetActive(false);
			clicked = 0;

			SendMessageUpwards("finishWorkStep", new MsgObject(gameObject.name, TaskConsts.CLICK_FAST));

			AnimationFunctions.diseaseSolvedFX(this.gameObject.transform.position);
			AnimationFunctions.diseaseSolvedFace();

        }
	}

    public void setEffort(int clickTimes)
    {
        this.clickTimes = clickTimes;
    }
}
