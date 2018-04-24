
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour
{
	public GameObject diskTemplate;

	private List<DiskData> usedDisk;
	private List<DiskData> freeDisk;
	private int number;

	private FirstController sceneController;

	public int GetDisk(int count) {
		/**
		 * Generate DiskData due to the Round (corresponding data stored in DiskFactory
		 * return the "generated" disk's id in List<DiskData> usedDisk;
		 * the returned id (that is the disk which is in use) will be recorded in the caller.
		 */
		DiskData aDisk;
		if (freeDisk.Count > 0) {
			aDisk = freeDisk.ToArray () [freeDisk.Count - 1];
			Rigidbody rig = aDisk.gameObject.GetComponent<Rigidbody> ();
			if (sceneController.currentMoveMode == FirstController.MoveMode.CCMove) {
				rig.isKinematic = true;
				rig.useGravity = false;
			} else {
				rig.isKinematic = false;
				rig.useGravity = true;
			}
			freeDisk.RemoveAt (freeDisk.Count - 1);
		} else {
			number++;
			GameObject newOne = Instantiate (diskTemplate) as GameObject;
			Rigidbody tRig = newOne.GetComponent<Rigidbody> ();
			tRig.isKinematic = (sceneController.currentMoveMode == FirstController.MoveMode.CCMove) ? true : false;
			tRig.detectCollisions = true;

			newOne.name = "Disk" + number.ToString ();
			aDisk = newOne.GetComponent<DiskData> ();

			aDisk.innernumber = number;
		}
		aDisk.set (getDiskDataByRound (count));
		usedDisk.Add (aDisk);

		return aDisk.indexInusedDisk = usedDisk.Count - 1;
	}

	/**
	 * 给定id（在usedDisk中的位置）
	 * 返回对应的GameObject
	 */
	public GameObject GetDiskObject(int id) {
		return usedDisk [id].gameObject;
	}
	/**
	 * 给定id（在usedDisk中的位置）
	 * freeDisk掉目标DiskData （从usedDisk中取出加入freeDisk）
	 */
	public void freeDisk(int id) {
		DiskData aDisk = usedDisk[id];
		if (aDisk == null) {
			throw new System.Exception ();
		} else {
			freeDisk.Add (aDisk);
			usedDisk.Remove (aDisk);
		}
	}

	public void freeDiskAllDisks ()
	{
		for (int i = usedDisk.Count - 1; i >= 0; i--) {
			DiskData disk = usedDisk [i];
			freeDisk.Add (disk);
			usedDisk.Remove (disk);
		}

		for (int i = 0; i < freeDisk.Count; i++) {
			freeDisk [i].transform.position = new Vector3 (3f * i, 3f * i, -20);
			freeDisk [i].gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		}
	}

	void Awake ()
	{
		usedDisk = new List<DiskData> ();
		freeDisk = new List<DiskData> ();
		number = 0;
		sceneController = Singleton<FirstController>.Instance;
	}


	private const float basicDiskSize = 0.6f;
	private static DiskData getDiskDataByRound(int count) {
		// size, color, speed, shoot score
		// size
		float size = basicDiskSize + 0.5f / count;

		// color
		float red = Random.Range (0f, 1f);  
		float green = Random.Range (0f, 1f);  
		float blue = Random.Range (0f, 1f); 
		Color color = new Color (red, green, blue);

		float speed = 10f + 5f * count;

		int score = 10 * count;

		return new DiskData (size, color, speed, score);
	}
}

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	protected static T instance;

	public static T Instance {
		get {
			if (instance == null) {
				instance = (T)FindObjectOfType (typeof(T));
				if (instance == null) {
					Debug.LogError("We need an instance of " + typeof(T), but there is none.");
				}
			}
			return instance;
		}
	}
}
