

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskData : MonoBehaviour
{
	public float size { get; set; }

	public Color color { get; set; }

	public float speed { get; set; }

	public int shotScore { get; set; }

	public SSAction curr;

	public DiskData(float size1, Color color1, float speed1, int score1) {
		this.size = size1;
		this.color = color1;
		this.speed = speed1;
		this.shotScore = score1;
	}

	public void set(DiskData newData) {
		this.size = newData.size;
		this.color = newData.color;
		this.speed = newData.speed;
		this.shotScore = newData.shotScore;

		this.gameObject.transform.localScale = new Vector3 (size, size, size);

		Renderer renders = this.transform.GetComponent<Renderer> ();
		renders.material.shader = Shader.Find ("Transparent/Diffuse");
		renders.material.color = this.color;
	}
		
	public int indexInUsed { get; set; }

	public int innerDiskCount { get; set; }
}
