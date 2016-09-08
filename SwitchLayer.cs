/* 
	Script zum Austauschen der Grafiken des Monsters wenn ein Layer-Switch passiert.
 */

using UnityEngine;
using System.Collections;


public class SwitchLayer : MonoBehaviour {
	//ALT - Konstanten für Filenames
	private string SKINLOC = "Sprites/Creature_Skin/";
	private string XRAYLOC = "Sprites/Creature_XRay/";

	public void switchGraphics(bool skin){
		SpriteRenderer[] bodyparts = this.GetComponentsInChildren<SpriteRenderer> ();
		string fileLoc = "";
		if (!skin) {
			fileLoc = XRAYLOC;
		} else {
			fileLoc = SKINLOC;
		}

		foreach(SpriteRenderer s in bodyparts){
			string name = s.name;
			string assetLocation = fileLoc+name;
			//Debug.Log(assetLocation);
			Sprite newSprite = Resources.Load<Sprite>(assetLocation);
			s.sprite = newSprite;
			
		}
	}
}
