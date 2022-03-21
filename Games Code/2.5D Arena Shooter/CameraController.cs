using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float distanceToTarget;

    Transform target;

	void Start () {
        target = GameObject.Find("Player").transform;
	}
	
	void FixedUpdate () {
        if (target != null)
        {
            Vector3 targetPosition = target.position - transform.forward * distanceToTarget;
            Vector3 actualPosition = Vector3.Lerp(transform.position, targetPosition, 5 * Time.fixedDeltaTime);
            transform.position = actualPosition;
        }
        else
        {
            Vector3 targetPosition = Vector3.zero - transform.forward * 20;
            Vector3 actualPosition = Vector3.Lerp(transform.position, targetPosition, 10 * Time.fixedDeltaTime);
            transform.position = actualPosition;
        }
    }
}
