using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CircleFollowing : MonoBehaviour {

	float mass = 1.0f;
	float maxSpeed = 5.0f;
	Vector3 velocity = Vector3.zero;
	Vector3 force = Vector3.zero;
	int currentWaypoint = 0;

	Vector3 target = Vector3.zero;

	Vector3 [] waypoints  = new Vector3 []
	{
		new Vector3(0,0,10),
		new Vector3(4,0,6),
		new Vector3(8,0,2),
		new Vector3(8,0,-2),
		new Vector3(4,0,-6),
		new Vector3(0,0,-10),
		new Vector3(-4,0,-6),
		new Vector3(-8,0,-2),
		new Vector3(-8,0,2),
		new Vector3(-4,0,6)

	};

	Vector3 Seek (Vector3 target)
	{
		Vector3 toTarget = target-transform.position;
		if(toTarget.magnitude<1.0f)
		{
			currentWaypoint++;
				
			if(currentWaypoint == waypoints.Length)
				{
					currentWaypoint = 0;
				}
		}

		toTarget.Normalize ();
		toTarget*=maxSpeed;

		return toTarget-velocity;
	}

	private void DrawPath()
	{
		Debug.DrawLine (waypoints[0],waypoints[1]);
		Debug.DrawLine (waypoints[1],waypoints[2]);
		Debug.DrawLine (waypoints[2],waypoints[3]);
		Debug.DrawLine (waypoints[3],waypoints[4]);
		Debug.DrawLine (waypoints[4],waypoints[5]);
		Debug.DrawLine (waypoints[5],waypoints[6]);
		Debug.DrawLine (waypoints[6],waypoints[7]);
		Debug.DrawLine (waypoints[7],waypoints[8]);
		Debug.DrawLine (waypoints[8],waypoints[9]);
		Debug.DrawLine (waypoints[9],waypoints[0]);
	}

	void Update ()
	{
		target = waypoints [currentWaypoint];
		force += Seek (target);

		Vector3 accel = force / mass;

		velocity = velocity + accel * Time.deltaTime;
		transform.position=transform.position + accel * Time.deltaTime;
		force = Vector3.zero;
		if(velocity.magnitude>float.Epsilon)
		{
			transform.forward=Vector3.Normalize (velocity);
		}
		velocity *= .99f;
	}
}
