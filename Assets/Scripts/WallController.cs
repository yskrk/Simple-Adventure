using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0) {
			// focus wall
			
			// remove wall
			Debug.Log("remove wall");
			Destroy(this.gameObject);
		}
    }
}