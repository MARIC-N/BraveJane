using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithDelay : MonoBehaviour {

	public float DelayTime=1.0f;
  
	void Start ()
    {
        Destroy(gameObject, DelayTime);
	}
}
