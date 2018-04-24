
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SSActionManager : MonoBehaviour
{
	private Dictionary <int, SSAction> actions = new Dictionary<int, SSAction> ();
	private List<SSAction> waitAdd = new List<SSAction> ();
	private List<int> waitDel = new List<int> ();

	protected FirstController sceneController;

	void Awake() {
		sceneController = Singleton<FirstController>.Instance;
	}

	protected void Update ()
	{
		if (sceneController.currentMoveMode == FirstController.MoveMode.CCMove) {
			if (sceneController.isPaused == false && sceneController.isStarted == true) {
				foreach (SSAction act in waitAdd)
					actions [act.GetInstanceID ()] = act;
				waitAdd.Clear ();

				foreach (KeyValuePair <int, SSAction> kv in actions) {
					SSAction act = kv.Value;
					if (act.destory) {
						waitDel.Add (act.GetInstanceID ());
					} else if (act.enable) {
						act.Update ();
					}
				}

				foreach (int _key in waitDel) {
					SSAction act = actions [_key];
					actions.Remove (_key);
					DestroyObject (act);
					sceneController.diskComp++;
				}
				waitDel.Clear ();
			}
		}
	}

	protected void FixedUpdate() {
		if (sceneController.currentMoveMode == FirstController.MoveMode.PhysicsMove) {
			if (sceneController.isPaused == false && sceneController.isStarted == true) {
				foreach (SSAction act in waitAdd)
					actions [act.GetInstanceID ()] = act;
				waitAdd.Clear ();

				foreach (KeyValuePair <int, SSAction> kv in actions) {
					SSAction act = kv.Value;
					if (act.destory) {
						waitDel.Add (act.GetInstanceID ());
					} else if (act.enable) {
						act.FixedUpdate ();
					}
				}

				foreach (int _key in waitDel) {
					SSAction act = actions [_key];
					actions.Remove (_key);
					DestroyObject (act);
					sceneController.diskComp++;
				}
				waitDel.Clear ();
			}
		}
	}

	public void ClearAction() {
		foreach (int _key in waitDel) {
			SSAction act = actions [_key];
			actions.Remove (_key);
			DestroyObject (act);
		}
		waitAdd.Clear ();
		waitDel.Clear ();
		actions.Clear ();
	}

	public void RunAction (GameObject gameObj, SSAction act, ISSActionCallback managers)
	{
		action.gameObject = gameObj;
		action.callback = managers;
		gameObject.GetComponent<DiskData> ().currentSSAction = act;
		waitAdd.Add (act);
		action.Start ();
	}

}
