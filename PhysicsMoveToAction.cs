
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMoveToAction : SSAction {

	public Vector3 target;
	public float speed;

	private int curTime;
	private int timeNum;

	private bool reachedEnd {
		get {
			return curTime >= timeNum;
		}
	}

	private bool reachedMid {
		get {
			return curTime >= timeNum / 2;
		}
	}

	public static PhysicsMoveToAction GetSSAction (float _speed)
	{
		PhysicsMoveToAction act = ScriptableObject.CreateInstance<PhysicsMoveToAction> ();
		act.speed = _speed;
		act.originPosition = SSAction.getRandomStartPoint ();
		return act;
	}

	public override void FixedUpdate ()
	{
		Rigidbody Rig = this.gameObject.GetComponent<Rigidbody> ();
		if (this.enable && !this.reachedEnd) {
			curTime++;
			if (Rig) {
				Rig.velocity = new Vector3 (Random.Range(-5f, 5f), (speed / 4) + Random.Range(-4f, 2f), speed);
			}
		}
		if (this.reachedEnd) {
			reset ();
			Rig.MovePosition (this.originPosition);
			Rig.velocity = Vector3.zero;
			this.callback.SSActionEvent (this);
		}
	}

	public void reset() {
		this.gameObject.transform.position = this.originPosition;
		curTime = 0;
	}

	public override void Start ()
	{
		this.gameObject.transform.position = this.originPosition;
		this.enable = true;

		timeNum = (int)(2f / Time.deltaTime);
		curTime = 0;
	}

}
