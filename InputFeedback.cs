/*
 * Long Duc Nguyen 
 * 
 * InputFeedback - Skript zum Auslösen eines Effekts welches nach jedem Touch ausgeführt wird.
 */

using UnityEngine;
using System.Collections;

public class InputFeedback : MonoBehaviour {


	void Update(){
		foreach (Touch t in Input.touches) {
			if (t.phase == TouchPhase.Began) {
				Instantiate (Resources.Load ("Prefabs/FX_Input") as GameObject, t.position, Quaternion.identity);
			}
		}
	}
}
