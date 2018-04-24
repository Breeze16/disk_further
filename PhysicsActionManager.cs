
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsActionManager :  SSActionManager, IActionManager, ISSActionCallback {


	private DiskFactory diskFactory;

	// Use this for initialization
	void Awake () {
		this.sceneController = Singleton<FirstController>.Instance;
		diskFactory = Singleton<DiskFactory>.Instance;
	}
		
	protected new void FixedUpdate() {
		if (sceneController.isPaused == true || sceneController.isStarted == false)
			return;
		base.FixedUpdate ();
	}

	public PhysicsMoveToAction ApplyMoveToAction(GameObject obj, float speed) {
		PhysicsMoveToAction temp = PhysicsMoveToAction.GetSSAction (speed);
		base.RunAction (obj, temp, this);
		return temp;
	}

	#region implementation of IActionManager
	public void playDisk() {
		GameObject obj = diskFactory.GetDiskObject (diskFactory.GetDisk (sceneController.round));
		this.ApplyMoveToAction (obj, sceneController.getSpeedByRound());
	}
	public void clearActions() {
		base.ClearAction ();
	}
	#endregion

	#region implementation of ISSActionCallback
	public void SSActionEvent (SSAction sor, SSActionEventType event = SSActionEventType.Compeleted,
		int num = 0,
		string str = null,
		Object obje = null) {

		sor.enable = false;
		sor.destory = true;
		sor.gameObject.transform.position = sor.originPosition;
	}
	#endregion
}
