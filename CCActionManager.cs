

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, IActionManager, ISSActionCallback {

	private DiskFactory factory;

	// Use this for initialization
	void Awake () {
		this.sceneController = Singleton<FirstController>.Instance;
		factory = Singleton<DiskFactory>.Instance;
	}

	protected new void Update () {
		base.Update ();
	}

	public CCMoveToAction ApplyMoveToAction(GameObject obj, float speed) {
		CCMoveToAction temp = CCMoveToAction.GetSSAction (speed);
		base.RunAction (obj, temp, this);
		return temp;
	}

	#region implementation of IActionManager
	public void playDisk() {
		GameObject obj = factory.GetDiskObject (factory.GetDisk (sceneController.round));
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
