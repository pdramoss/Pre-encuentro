using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : MonoBehaviour
{
    
    public float current_speed = 0.5f;
    public float whale_speed = 2f;

    private Rigidbody2D rb;
    private bool facing_right = true;
    private Vector2 move_direction;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        move_direction.x = Input.GetAxis("Horizontal");
        move_direction.y = Input.GetAxis("Vertical");
        transform.Translate(current_speed * Time.deltaTime, 0f, 0f);
        
        Camera.main.transform.Translate(current_speed * Time.deltaTime, 0f, 0f);

        if (move_direction.x > 0 && !facing_right)
            FlipCharacter();
        if (move_direction.x < 0 && facing_right)
            FlipCharacter();

        rb.velocity = new Vector2(move_direction.x * whale_speed, move_direction.y * whale_speed);

    }

    void FlipCharacter()
    {
        facing_right = !facing_right;
        transform.Rotate (0f, 180f, 0f);
    }
}
