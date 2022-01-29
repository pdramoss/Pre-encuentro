using UnityEngine;
using System.Collections;

public class Scannable : MonoBehaviour {
	private float timeEnable = 2;
	private float timeDisable = 6;
	private float timeRemaining = 0;
	private float minimumAlpha = 0.0625f;
    private bool isTimeRunning = false;
	private bool isEnabledItem = false;

	private void Start() {
		DisableItem();
	}
	
	private void Update() {
		if (isTimeRunning) {
			if (timeRemaining > 0) {
				timeRemaining -= Time.deltaTime;
				SetupMeshMaterialAlpha(timeRemaining, isEnabledItem);
			} else {
				if (isEnabledItem) {
					DisableItem();
				} else {
					timeRemaining = 0;
					isTimeRunning = false;
				}

			}
		}
	}

	private void SetupMeshMaterialAlpha (float timeRemaining, bool isEnabledItem) {
		float alpha = 0;
		if (isEnabledItem) {
			alpha = (timeEnable - timeRemaining) / timeEnable;
		} else {
			alpha = (timeRemaining / timeDisable);
		}
		MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
		Color color = meshRenderer.material.color;
		color.a = alpha;
		meshRenderer.material.color = color;
	}

	public void Ping() {
		MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
		float alpha = meshRenderer.material.color.a;

		if (alpha <= minimumAlpha) {
			EnableItem();
		}
	}

	private void EnableItem() {
		isEnabledItem = true;
		isTimeRunning = true;
		timeRemaining = timeEnable;
	}

	private void DisableItem() {
		isEnabledItem = false;
		isTimeRunning = true;
		timeRemaining = timeDisable;
	}
}
