using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WhaleController : MonoBehaviour {
    private PlayerController playerController;
    // Start is called before the first frame update

    private void Awake() {
        playerController = new PlayerController();
    }

    void Start() {
        
    }

    public void TestSonar(InputAction.CallbackContext context) {
        Debug.Log("SONAR" + context.ReadValueAsButton());
    }

    // Update is called once per frame
    private void Update() {
    }

    private void OnEnable() {
        playerController.Enable();
        playerController.Ocean.Sonar.performed += TestSonar;
    }

    private void OnDisable() {
        playerController.Disable();
        playerController.Ocean.Sonar.performed -= TestSonar;
    }
}
