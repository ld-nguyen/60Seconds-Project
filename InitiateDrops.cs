/*
 * Long Duc Nguyen
 * 
 * Initiate Drops - Initiiert die Tropfen mit Parametern die man im Editor einstellen kann.
 */

using UnityEngine;
using System.Collections;

public class InitiateDrops : MonoBehaviour {
	public GameObject prefab; //Prefab des Tropfens
	public GameObject originObject; //Ausgangs Objekt des Tropfens 
	public float pauseTimer; // Zeit zwischen den Tropfen 
	private float timer; 

	void Start(){
	}
	void Update () {
		timer += Time.deltaTime;
		if (timer >= pauseTimer) {
			Instantiate (prefab, originObject.transform.position, originObject.transform.rotation);
			timer = 0;
		}
	}
}
