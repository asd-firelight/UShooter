using UnityEngine;
using System.Collections;

public class ForceZ : MonoBehaviour {

    float z = 0;
	
	void Start () {

        z = transform.position.z;
	
	}
	
	void Update () 
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, z);	
	}
}
