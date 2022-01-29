using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFollower : MonoBehaviour
{
    
    public float speed = 0.1f;

    // Pública por si queremos que otro bicho siga a otra cosa
    public GameObject character = null;

    private bool facing_right = true;
    private Transform target;

    void Start()
    {
        target = character.GetComponent<Transform>();
    }

    
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        Debug.Log(target.position);
        if (target.position.x > transform.position.x && !facing_right)
            FlipCharacter();
        if (target.position.x < transform.position.x && facing_right)
            FlipCharacter();
    }

    void FlipCharacter()
    {
        facing_right = !facing_right;
        transform.Rotate (0f, 180f, 0f);
    }
}
