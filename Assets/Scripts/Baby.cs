using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : MonoBehaviour
{
    public int health = 100;
    public bool under_pressure = false;

    private Animator animator;

    private SpriteRenderer sprite;
    private IEnumerator pressure_harm;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        pressure_harm = PressureHarm();
    }
    
    void Update()
    {
        if (this.transform.position.y < -1)
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

    void ResetColor()
    {
        sprite.color = new Color(1, 1, 1, 1);
    }

    public void Harm(int damage){
        AudioManager.PlaySound(AudioManager.Sound.sfx_baby_damage, false, 0.5f);
        sprite.color = new Color(1, 0.2f, 0.2f, 1);
        health = health - damage;
        Debug.Log("Bellenita tiene: " + health);
        Invoke("ResetColor", 0.1f);
        if (health <= 0){
            Die();
        }
    }

    

    public void Die(){
        animator.SetTrigger("Die");
        Debug.Log("Infanticidio");
        GetComponent<CharacterFollower>().speed = 0;
        GameManager.sharedInstance.GameOver();
    }

    private IEnumerator PressureHarm()
    {
        while(true)
        {
            yield return new WaitForSeconds(3);
            Harm(5*2);
        }
    }
}
