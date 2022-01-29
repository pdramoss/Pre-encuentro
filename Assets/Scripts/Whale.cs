using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : MonoBehaviour
{
    
    public float ocean_speed = 0.5f;
    public float whale_speed = 2f;
    public bool can_move = true;

    private Rigidbody2D rb;
    private bool facing_right = true;
    private Vector2 move_direction;
    public Vector3 startPosition;

    private int healthPoints;

    public const int Initial_Health = 100, Max_Health = 200;

    public const int Max_dist= 1000;
    
    void Start()
    {
        Physics.IgnoreLayerCollision(4, 0);
        rb = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
    }

    public void StartGame(){
        this.transform.position = startPosition;

        healthPoints = Initial_Health;
    }

    void Update()
    {
        move_direction.x = Input.GetAxis("Horizontal");
        move_direction.y = Input.GetAxis("Vertical");
    
        if (move_direction.x > 0 && !facing_right)
            FlipCharacter();
        if (move_direction.x < 0 && facing_right)
            FlipCharacter();

        rb.velocity = new Vector2(move_direction.x * whale_speed, move_direction.y * whale_speed);
        transform.Translate(ocean_speed * Time.deltaTime, 0f, 0f, Space.World);

    }

    void FlipCharacter()
    {
        facing_right = !facing_right;
        transform.Rotate (0f, 180f, 0f);
    }

    public void Die(){
        GameManager.sharedInstance.GameOver();
    }

    public void CollectHealth(int points){
        this.healthPoints += points;
        if(this.healthPoints >= Max_Health){
            this.healthPoints = Max_Health;
        }
    }

    public int GetHealth(){
        return healthPoints;
    }

    public float GetDistance (){
            return this.transform.position.x - startPosition.x;
    }

}
