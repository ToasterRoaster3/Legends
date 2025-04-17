using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Collider[] weapons;

    private CharacterController characterController;
    private Animator animator;

    private Vector3 targetPosition;

    private float moveSpeed = 7;
    // Start is called before the first frame update

    public void ToggleWeapons(bool enable)
    {
        foreach (Collider weapon in weapons)
        {
            weapon.enabled = enable;
        }
    }
    
    void Start()
    {
        ToggleWeapons(false);
        characterController = GetComponent <CharacterController>();
        animator = GetComponent<Animator>();
        targetPosition = transform.position;
    }
    
    public void BeginAttack()
    {
        ToggleWeapons(true);
    }

    public void EndAttack()
    {
        ToggleWeapons(false);
    }

    // Update is called once per frame
    void Update()
    {
        float distToTarget= Vector3.Distance(targetPosition, transform.position);
        if (distToTarget > 0.5f && PlayerHealth.isAlive)
        {
            Vector3 direction = Vector3.Normalize(targetPosition - transform.position);
            characterController.Move(direction * moveSpeed * Time.deltaTime);
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
        if (Input.GetMouseButtonDown(0) && PlayerHealth.isAlive)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 500, layerMask))
            {
                Debug.Log("Hit: " + hit.collider.name);
                targetPosition = hit.point;
                transform.LookAt(targetPosition);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("Stab");
        }
    }
}
