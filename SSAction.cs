
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SSActionEventType:int
{
	Started,
	Compeleted
}

public interface ISSActionCallback
{
	void SSActionEvent (SSAction source, SSActionEventType events = SSActionEventType.Compeleted,
	                    int num = 0,
	                    string str = null,
	                    Object obj = null);
}

public class SSAction : ScriptableObject
{

	public bool enable = true;
	public bool destory = false;

	public GameObject gameObject { get; set; }

	public Vector3 originPosition = new Vector3 (0, 0, -20);

	public ISSActionCallback callback { get; set; }

	protected SSAction ()
	{
	}

	// Use this for initialization
	public virtual void Start ()
	{
		throw new System.NotImplementedException ();
	}
	
	// Update is called once per frame
	public virtual void Update ()
	{
		throw new System.NotImplementedException ();
	}

	public virtual void FixedUpdate ()
	{
		throw new System.NotImplementedException ();
	}

	public static Vector3 getRandomStartPoint ()
	{
		return new Vector3 (Random.Range (-1.5f, 1.5f), 1.5f, -12f);
	}

	public static Vector3 getRandomEndPoint ()
	{
		return new Vector3 (Random.Range (-2f, 2f), Random.Range (1f, 6f), 5f);
	}
}
