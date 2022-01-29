using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {
    // Start is called before the first frame update
    public Rigidbody enemy;
    private float timeRemaining = 10;
    public bool timeIsRunning = false;
    
    private void Start() {
        timeIsRunning = true;
    }

    // Update is called once per frame
    void Update() {
        if (timeIsRunning) {
            if (timeRemaining > 0) {
                timeRemaining -= Time.deltaTime;
            } else {
                timeRemaining = 10;
                instantiateEnemy();
            }
        }
        
    }

    private void instantiateEnemy() {
        Rigidbody clone = Instantiate(enemy, transform.position, transform.rotation);
        clone.velocity = transform.TransformDirection(Vector3.left * 30);
    }
}
