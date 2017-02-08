using UnityEngine;
using System.Collections;

public class TrailVector : MonoBehaviour 
{
    public float Speed = 10.0f;
    public Vector3 Vec = Vector3.zero;

    public float Life = 0.1f;

	// Update is called once per frame
	void Update () 
    {
        Life -= Time.deltaTime;

        if (Life <= 0)
        {
            GameObject.Destroy(gameObject);
        }

        transform.position += Vec * Speed * Time.deltaTime;
	
	}
}
