using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : MonoBehaviour
{
    
    public float ocean_speed = 0.5f;
    public float whale_speed = 2f;
    public int health = 100; 
    public bool can_move = true;
    public Vector3 startPosition;
    public bool under_pressure = false;

    private Rigidbody2D rb;
    private bool facing_right = true;
    private Vector2 move_direction;
    private SpriteRenderer sprite;
    private IEnumerator pressure_harm;
    
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
        pressure_harm = PressureHarm();
    }

    void StartGame(){
        this.transform.position = startPosition;
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

        if (this.transform.position.y < 0)
        {
            if (!under_pressure){
                StartCoroutine(pressure_harm);
                under_pressure = true;
            }
        }
        else
        {
            if (under_pressure){
            StopCoroutine(pressure_harm);
            under_pressure = false;
            }
        }

    }

    void FlipCharacter()
    {
        facing_right = !facing_right;
        transform.Rotate (0f, 180f, 0f);
    }
    
    void ResetColor()
    {
        sprite.color = new Color(1, 1, 1, 1);
    }

    public void Harm(int damage){
        sprite.color = new Color(1, 0.2f, 0.2f, 1);
        health = health - damage;
        Debug.Log("Ballena tiene: " + health);
        Invoke("ResetColor", 0.1f);
        if (health <= 0){
            Die();
        }
    }

    public void Die(){
        Debug.Log("Ballenicidio");
        GameManager.sharedInstance.GameOver();
    }

    private IEnumerator PressureHarm()
    {
        while(true)
        {
            yield return new WaitForSeconds(3);
            Harm(5);
        }
    }
}
