using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSceneLine : MonoBehaviour {

	public Transform from;
	public Transform to;

	void OnDrawGizmosSelected(){
		if (from != null && to != null){
			Gizmos.color = Color.red;
			Gizmos.DrawLine(from.position, to.position);
			Gizmos.DrawSphere(from.position, 0.15f);
			Gizmos.DrawSphere(to.position, 0.15f);
		}
	}
}
