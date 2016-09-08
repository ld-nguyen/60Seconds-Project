/*
 * Long Duc Nguyen
 * 
 * Drop - Skript welches einen Tropfen vereinfacht simuliert
 */

using UnityEngine;
using System.Collections;

public class Drop : MonoBehaviour {
	public float duration; //Dauer, wie lange der Tropfen da ist
	public float deltaDistace; //Modifikator für die Tropfweite pro Frame 
	public float fadeStartTime; //Zeit an dem der Partikel anfangen soll zu faden
	public GameObject particle; 
	private SpriteRenderer sprite;

	void Start(){
		sprite = particle.GetComponent<SpriteRenderer>();
	}

	void Update () {
		duration -= Time.deltaTime;
		if (duration >= 0) { 
						Vector3 oldPos = particle.transform.position; //Momentane Position
						Vector3 newPos = new Vector3 (oldPos.x, oldPos.y + deltaDistace * Time.deltaTime, oldPos.z); //Neue berechnete Position
						if (duration <= fadeStartTime) {
								Color newColor = sprite.color; 
								float newAlpha = Mathf.Lerp (sprite.color.a, 0, Time.deltaTime); //neues Alpha berechnen mithilfe von Lerp
								newColor.a = newAlpha;
								sprite.color = newColor;
						}
						particle.transform.position = newPos; 
				} else {
					GameObject.Destroy(particle);
				}
	}
}
