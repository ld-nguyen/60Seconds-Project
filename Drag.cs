/*
 * Drag - Laura Eckhardt
 * 
 * Methode highlightFracture von Long Duc Nguyen
 */
using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour
{
    private Vector3 screenOffset;
    private float screenPointZ;

	private SpriteRenderer[] fractureParts;
	private string origSpriteName;
    void Start()
    {
        screenPointZ = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
		fractureParts = this.transform.parent.gameObject.GetComponentsInChildren<SpriteRenderer> ();
		Debug.Log (this.transform.parent.gameObject);
	}

    void Update()
    {
            foreach (Touch touch in Input.touches)
            {
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0));
                if (collider2D.bounds.IntersectRay(ray))
                {
                    if (touch.phase == TouchPhase.Began)
                    {
						screenOffset = Camera.main.WorldToScreenPoint(gameObject.transform.position) - new Vector3(touch.position.x, touch.position.y, screenPointZ);
                        Screen.showCursor = false;
						highlightFracture(true);
                        
                    }
                    else if (touch.phase == TouchPhase.Moved)
                    {
                        Vector3 curScreenPoint = new Vector3(touch.position.x, touch.position.y, screenPointZ);
                        Vector3 curScreenPosition = curScreenPoint + screenOffset;
                        transform.position = Camera.main.ScreenToWorldPoint(curScreenPosition);
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
			           Screen.showCursor = true;
						highlightFracture(false);
                    }
                }
            }
        }

 // Hebt beide Knochenteile vor beim Berühren eines Knochenteils
	private void highlightFracture(bool enable){

		if (enable) {
			foreach (SpriteRenderer sp in fractureParts) {
				//GetComponentsInChildren() in Line 14 gibt auch Components im "parent"-Objekt zurück. Der If-Block ist zum Filtern da damit er nicht verändert wird. 
				if(sp.gameObject.name != "Fracture"){ 
					Sprite newSprite = Resources.Load<Sprite> ("Sprites/Creature_Bone/Fracture/" + sp.gameObject.name + "_highlight");
					sp.sprite = newSprite;
				}
			}
		} else {
			foreach (SpriteRenderer sp in fractureParts)	{
				//siehe oben
				if(sp.gameObject.name != "Fracture"){ 
					Sprite newSprite = Resources.Load<Sprite> ("Sprites/Creature_Bone/Fracture/" + sp.gameObject.name);
					sp.sprite = newSprite;
				}
			}	
		}
	}
}

