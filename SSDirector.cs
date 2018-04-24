
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSDirector : System.Object
{
	private static SSDirector ins;

	public ISceneController currentSceneController { get; set; }

	public bool running { get; set; }

	public static SSDirector getInstance ()
	{
		if (ins == null) {
			ins = new SSDirector ();
		}
		return ins;
	}

	public int getFPS ()
	{
		return Application.targetFrameRate;
	}

	public void setFPS (int _fps)
	{
		Application.targetFrameRate = _fps;
	}

	public void NextScene() {}
}
