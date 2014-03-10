using UnityEngine;
using System.Collections;

public class RandomCube : MonoBehaviour {

	Vector3 velocity = Vector3.zero;
	Vector3 force = Vector3.zero;
	float maxSpeed = 2.0f;
	float mass = 1.0f;

	public Vector3 target = Vector3.zero;

	Vector3 ArriveAtTarget (Vector3 target)
	{
		Vector3 toTarget = target - transform.position;
		float arriveDist = 1.0f;
		float decel = 1.0f;
		if(toTarget.magnitude<1.0f)
		{
			return Vector3.zero;
		}
		else
		{
			float rSpeed = maxSpeed*(toTarget.magnitude/(decel*arriveDist));
			float cSpeed = Mathf.Min (maxSpeed,rSpeed);
			toTarget*=cSpeed;

			return toTarget - velocity;
		}

	}

	void Start ()
	{
		float a = Random.Range (-10, 10);
		float b = Random.Range (-10, 10);

		float e=0;
		float d=0;

		if (a<0)
		{
			d = a*-1;
		}

		if (b<0)
		{
			e = b*-1;
		}

		if(d+e > 10)
		{
			float c = d+e - 10;
			if(d>e)
			{
				if(a>0)
				{
					a=a-c;
				}
				if(a<0)
				{
					a=a+c;
				}
			}
			else if(d<e)
			{
				if(b>0)
				{
					b=b-c;
				}
				if(b<0)
				{
					b=b+c;
				}
			}
		}

		target = new Vector3 (a,0,b);
	}

	void Update ()
	{
		force += ArriveAtTarget (target);
		
		Vector3 accel = force / mass;
		
		velocity = velocity + accel * Time.deltaTime;
		transform.position=transform.position + accel * Time.deltaTime;
		if (force.magnitude<1f)
		{
			Start ();
		}
		force = Vector3.zero;
		if(velocity.magnitude>float.Epsilon)
		{
			transform.forward=Vector3.Normalize (velocity);
		}
		velocity *= .99f;
	}
}
