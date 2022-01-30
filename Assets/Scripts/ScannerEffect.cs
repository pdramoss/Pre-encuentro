using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class ScannerEffect : MonoBehaviour {
	public Transform scannerOrigin;
	public Material effectMaterial;
	public float scanDistance;
	public float maximumDistance;

	private Camera _camera;

	// Demo Code
	private bool _scanning;
	private Scannable[] _scannables;

	private PlayerController playerController;
    // Start is called before the first frame update

    private void Awake() {
        playerController = new PlayerController();
    }

	void Start() {
		_scannables = FindObjectsOfType<Scannable>();
    }

	private void Update() {
		if (_scanning) {
			scanDistance += Time.deltaTime * 50;
			foreach (Scannable s in _scannables) {
				if (Vector3.Distance(scannerOrigin.position, s.transform.position) <= scanDistance)
					s.Ping();
			}

			if (scanDistance > maximumDistance) {
				_scanning = false;
				scanDistance = 0;
			}
		}
	}
	// End Demo Code

	private void OnEnable() {
		_camera = GetComponent<Camera>();
		_camera.depthTextureMode = DepthTextureMode.Depth;
		playerController.Enable();
		playerController.Ocean.Sonar.performed += ActivateSonar;
	}

	private void OnDisable() {
		playerController.Disable();
		playerController.Ocean.Sonar.performed -= ActivateSonar;
	}

	private void ActivateSonar(InputAction.CallbackContext context) {
		_scannables = FindObjectsOfType<Scannable>();
		_scanning = true;
		scanDistance = 0;
    }

	[ImageEffectOpaque]
	private void OnRenderImage(RenderTexture src, RenderTexture dst) {
		effectMaterial.SetVector("_WorldSpaceScannerPos", scannerOrigin.position);
		effectMaterial.SetFloat("_ScanDistance", scanDistance);
		RaycastCornerBlit(src, dst, effectMaterial);
	}

	private void RaycastCornerBlit(RenderTexture source, RenderTexture dest, Material mat) {
		// Compute Frustum Corners
		float camFar = _camera.farClipPlane;
		float camFov = _camera.fieldOfView;
		float camAspect = _camera.aspect;

		float fovWHalf = camFov * 0.5f;

		Vector3 toRight = _camera.transform.right * Mathf.Tan(fovWHalf * Mathf.Deg2Rad) * camAspect;
		Vector3 toTop = _camera.transform.up * Mathf.Tan(fovWHalf * Mathf.Deg2Rad);

		Vector3 topLeft = (_camera.transform.forward - toRight + toTop);
		float camScale = topLeft.magnitude * camFar;

		topLeft.Normalize();
		topLeft *= camScale;

		Vector3 topRight = (_camera.transform.forward + toRight + toTop);
		topRight.Normalize();
		topRight *= camScale;

		Vector3 bottomRight = (_camera.transform.forward + toRight - toTop);
		bottomRight.Normalize();
		bottomRight *= camScale;

		Vector3 bottomLeft = (_camera.transform.forward - toRight - toTop);
		bottomLeft.Normalize();
		bottomLeft *= camScale;

		// Custom Blit, encoding Frustum Corners as additional Texture Coordinates
		RenderTexture.active = dest;

		mat.SetTexture("_MainTex", source);

		GL.PushMatrix();
		GL.LoadOrtho();

		mat.SetPass(0);

		GL.Begin(GL.QUADS);

		GL.MultiTexCoord2(0, 0.0f, 0.0f);
		GL.MultiTexCoord(1, bottomLeft);
		GL.Vertex3(0.0f, 0.0f, 0.0f);

		GL.MultiTexCoord2(0, 1.0f, 0.0f);
		GL.MultiTexCoord(1, bottomRight);
		GL.Vertex3(1.0f, 0.0f, 0.0f);

		GL.MultiTexCoord2(0, 1.0f, 1.0f);
		GL.MultiTexCoord(1, topRight);
		GL.Vertex3(1.0f, 1.0f, 0.0f);

		GL.MultiTexCoord2(0, 0.0f, 1.0f);
		GL.MultiTexCoord(1, topLeft);
		GL.Vertex3(0.0f, 1.0f, 0.0f);

		GL.End();
		GL.PopMatrix();
	}
}