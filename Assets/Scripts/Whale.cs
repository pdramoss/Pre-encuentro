using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Whale : MonoBehaviour
{
    
    public float ocean_speed = 0.5f;
    public float whale_speed = 2f;
    public bool can_move = false;
    public Vector3 startPosition;
    public bool under_pressure = false;
    public const int Initial_Health = 100, Max_Health = 200;
    public const int Max_dist= 10;

    private Rigidbody2D rb;
    private Vector2 move_direction;

    private Animator animator;

    private SpriteRenderer sprite;
    private IEnumerator pressure_harm;
    private bool facing_right = true;
    private bool moving = false;

    private int healthPoints;
    private bool low_health = false; 


    
    void Start()
    {   
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
        pressure_harm = PressureHarm();
        healthPoints = Initial_Health;
    }

    public void StartGame(){
        can_move = true;
        this.transform.position = startPosition;
        healthPoints = Initial_Health;
        animator.SetTrigger("Live");
    }

    void Update()
    {
        move_direction.x = Input.GetAxis("Horizontal");
        move_direction.y = Input.GetAxis("Vertical");
    
        if (move_direction.x > 0 && !facing_right)
            FlipCharacter();
        if (move_direction.x < 0 && facing_right)
            FlipCharacter();

        if (can_move)
        {
        rb.velocity = new Vector2(move_direction.x * whale_speed, move_direction.y * whale_speed);
        transform.Translate(ocean_speed * Time.deltaTime, 0f, 0f, Space.World);
        }

        if (move_direction.x != 0 )
        {
            if (!moving){
                AudioManager.PlaySound(AudioManager.Sound.sfx_whale_head, false, 0.2f);
                AudioManager.PlaySound(AudioManager.Sound.sfx_whale_loop, true, 0.8f);
                animator.SetFloat("SwimSpeed", 1.4f);
                moving = true;
            }
        }
        else
        {
            if (moving){
                if (can_move){
                    AudioManager.PlaySound(AudioManager.Sound.sfx_whale_tail, false, 0.8f);
                    AudioManager.StopSound(AudioManager.Sound.sfx_whale_loop);
                }
                animator.SetFloat("SwimSpeed", 1f);
                moving = false;
            }    
        }

        if (this.transform.position.y < -0.6f)
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
        AudioManager.PlaySound(AudioManager.Sound.sfx_whale_damage, false, 1.2f);
        sprite.color = new Color(255, 100, 100, 0);
        this.healthPoints -= damage;
        Debug.Log("Ballena tiene: " + healthPoints);
        Invoke("ResetColor", 0.1f);
        if (healthPoints <= 30 && healthPoints > 0){
            if (!low_health){
                AudioManager.PlaySound(AudioManager.Sound.sfx_danger_loop, true, 1f);
                low_health = true;
            }
        }
        if (healthPoints <= 0){
            Die();
        }
    }

    public void Die(){
        AudioManager.PlaySound(AudioManager.Sound.sfx_whale_die, false, 1f);
        animator.SetTrigger("Die");
        if (low_health){
                AudioManager.StopSound(AudioManager.Sound.sfx_danger_loop);
            }
        Debug.Log("Ballenicidio");
        can_move = false;
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
        if (healthPoints > 30){
            if (low_health){
                AudioManager.StopSound(AudioManager.Sound.sfx_danger_loop);
                low_health = false;
            }
        }
        Debug.Log("Ballena tiene: " + healthPoints);
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

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Finish"){
               Debug.Log("Terminó el nivel");
               AudioManager.StopSound(AudioManager.Sound.sfx_whale_loop);
            GameManager.sharedInstance.Winner();
        }
    }

   
    
}
