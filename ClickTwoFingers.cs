/*
 * ClickTwoFingers - 90% Laura Eckhardt 10 % Long Duc Nguyen
 */
using UnityEngine;
using System.Collections;

public class ClickTwoFingers : MonoBehaviour {

    private int clickTimes = 10;

    private int clicks;
    private float time0;
    private float time1;

    private Transform child0;
    private Transform child1;

	private Transform car;
	private float yPosCar;
	private float origYPos;

    void Start()
    {
        clicks = 0;
        time0 = 0;
        time1 = 0;

        child0 = transform.GetChild(0);
        child1 = transform.GetChild(1);

		car = transform.GetChild (2);
		yPosCar = car.position.y;
		origYPos = car.position.y;
    }

    // compares clicktimes of two sprites and counts them if they are almost the same
    void Update()
    {
        // times also need to be differ, if no lung is clicked
        time0 = -10;

        foreach(Touch touch in Input.touches){

            if (touch.phase == TouchPhase.Ended)
            {
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0));

                if (child0.renderer.bounds.IntersectRay(ray))
                {
                  //  Debug.Log("c0");
                    time0 = Time.time;
                }

                if (child1.renderer.bounds.IntersectRay(ray))
                {
                  //  Debug.Log("c1");
                    time1 = Time.time;
                }

                // Abstand zwischen den Clicks auf beiden Lungenflügeln darf nicht zu groß sein
                if (Mathf.Abs(time0 - time1) <= 0.01)
                {
                    clicks++;
					AnimationFunctions.coughFaceAnim();
					yPosCar += 5.0f;
					car.position = new Vector3(car.position.x,yPosCar,car.position.z);
                    Debug.Log("click");
                }
            }
            if (clicks == 10) {
	            // resets sprite, collider and script
	            //SpriteRenderer renderer0 = child0.GetComponent<SpriteRenderer>();
	            //SpriteRenderer renderer1 = child1.GetComponent<SpriteRenderer>();
	            //renderer0.sprite = Resources.Load("Sprites/Organs/Lungs_0", typeof(Sprite)) as Sprite; // heart
	            //renderer1.sprite = Resources.Load("Sprites/Organs/Lungs_1", typeof(Sprite)) as Sprite; // heart

	            Destroy(gameObject.GetComponent<ClickTwoFingers>());

				World.getInstance().DiseaseSystem.displayGoodGameObject(gameObject);
				gameObject.SetActive(false);
				car.position = new Vector3(car.position.x,origYPos,car.position.z); // Reset des Autos
				yPosCar = origYPos;

				SendMessageUpwards("finishWorkStep", new MsgObject(gameObject.name, TaskConsts.CLICK_TWO_FINGERS));

				AnimationFunctions.diseaseSolvedFX(this.gameObject.transform.position);
				AnimationFunctions.diseaseSolvedFace();

            }

        }
    }

    public void setEffort(int clickTimes)
    {
        this.clickTimes = clickTimes;
    }

}
