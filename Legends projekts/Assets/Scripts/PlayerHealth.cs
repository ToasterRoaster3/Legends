using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth;
    [SerializeField] private float hitInterval = 0.5f;

    private float lastHitTime = 0;
    private int currentHealth;

    private Animator animator;

    public static bool isAlive = true;
    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
        isAlive = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyWeapon") && isAlive && Time.time - lastHitTime > hitInterval)
        {
            TakeDamage(5);
        }
    }

    private void TakeDamage(int damage)
    {
        lastHitTime = Time.time;
        currentHealth -= damage;
        Debug.Log("Current health: " + currentHealth);
        
        if (currentHealth > 0)
        {
            animator.SetTrigger("Hit");
        }
        else
        {
            animator.SetTrigger("Death");
            isAlive = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
