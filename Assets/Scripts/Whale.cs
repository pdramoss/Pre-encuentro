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
    private Vector2 move_direction;

    private SpriteRenderer sprite;
    private IEnumerator pressure_harm;
    private bool facing_right = true;
    private bool moving = false;

    private int healthPoints;

    public const int Initial_Health = 100, Max_Health = 200;

    public const int Max_dist= 1000;
    
    void Start()
    {
        //ESTE DEBERIA IR EN EL SCRIPT DEL NIVEL
        AudioManager.PlaySound(AudioManager.Sound.mxIngame, true, 0.6f);
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
        pressure_harm = PressureHarm();
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

        if (move_direction.x != 0 )
        {
            if (!moving){
                AudioManager.PlaySound(AudioManager.Sound.sfx_whale_head, false, 0.7f);
                AudioManager.PlaySound(AudioManager.Sound.sfx_whale_loop, true, 0.6f);
                moving = true;
            }
        }
        else
        {
            if (moving){
                AudioManager.PlaySound(AudioManager.Sound.sfx_whale_tail, false, 0.7f);
                AudioManager.StopSound(AudioManager.Sound.sfx_whale_loop);
                moving = false;
            }    
        }

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
        AudioManager.PlaySound(AudioManager.Sound.sfx_whale_damage, false, 1f);
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
