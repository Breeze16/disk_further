

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCMoveToAction : SSAction
{
	public Vector3 tar;
	public float speed;

	private int num;
	private int curNum;

	private float shotSpeed;
	private float time;
	private Vector3 pointA;
	private Vector3 pointB;
	private float gra = -10f;
	private Vector3 speed3;
	private Vector3 Gravity;
	private Vector3 curAngle;
	private float d_time = 0;

	public bool reachedEnd {
		get {
			return this.curNum >= num;
		}
	}

	public override void Start ()
	{
		num = (int)(2f / Time.deltaTime);
		curNum = 0;
		this.enable = true;

		pointA = getRandomStartPoint ();
		pointB = getRandomEndPoint ();
		time = Vector3.Distance (pointA, pointB) / shotSpeed;
		this.gameObject.transform.position = pointA;
		speed3 = new Vector3 ((pointB.x - pointA.x) / time,
			(pointB.y - pointA.y) / time - 0.5f * gra * time, (pointB.z - pointA.z) / time);
		Gravity = Vector3.zero;
	}

	public static CCMoveToAction GetSSAction (float _speed)
	{
		CCMoveToAction act = ScriptableObject.CreateInstance<CCMoveToAction> ();
		act._speed = act.shotSpeed = _speed;
		return act;
	}

	public override void Update ()
	{
		if (this.enable && this.curNum < this.num && this.gameObject.transform.position != pointB) {
			curNum++;
			Gravity.y = gra * (d_time += Time.fixedDeltaTime);
			this.gameObject.transform.position += (speed3 + Gravity) * Time.fixedDeltaTime;
			curAngle.x = -Mathf.Atan ((speed3.y + Gravity.y) / speed3.z) * Mathf.Rad2Deg;
			this.gameObject.transform.eulerAngles = curAngle;
		}
		if (this.reachedEnd) {
			reset ();
			this.callback.SSActionEvent (this);
		}
	}

	public void reset() {
		this.enable = false;
		this.gameObject.transform.position = pointA;
		speed3 = Vector3.zero;
		Gravity = Vector3.zero;
		curAngle = Vector3.zero;
		this.gameObject.transform.eulerAngles = curAngle;
		curNum = 0;
		d_time = 0;
	}

}
