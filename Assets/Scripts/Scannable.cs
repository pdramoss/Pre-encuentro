using UnityEngine;
using System.Collections;

public class Scannable : MonoBehaviour
{
	public GameObject gameObject;
	public void Ping() {
		gameObject.SetActive(true);
		var cubeRenderer = gameObject.GetComponent<Renderer>();
		cubeRenderer.material.SetColor("_Color", Color.red);
    }
}
