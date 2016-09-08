/**
 * Skript zum Abspielen einer Anim in einer Animation-Komponente eines Objektes nach einem Touch
*/

using UnityEngine;
using System.Collections;

public class TapAnimation : MonoBehaviour {

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0));
            if (renderer.bounds.IntersectRay(ray))
            {
                this.gameObject.animation.Play();
            }
        }
    }
}
