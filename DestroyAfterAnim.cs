/*
 * Long Duc Nguyen
 * 
 * DestroyAfterAnim - Funktionen die am Ende einer Animation ein GameObject mit Hilfe der AnimationEvents deaktiviert bzw. entfernt.
 * 
 */

using UnityEngine;
using System.Collections;

public class DestroyAfterAnim : MonoBehaviour {
	public void destroy(){
		GameObject.Destroy (this.gameObject);
	}
	public void deactivate(){
		this.gameObject.SetActive(false);
	}
}
