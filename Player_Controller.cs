using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private bool isMoving;
    private Vector2 input;
    private Animator animator;
    private BoxCollider2D boxCollider;

    [SerializeField] private LayerMask solidObjectLayer;
    [SerializeField] private LayerMask interactableLayer;

    private Vector3 facingDir;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        // Adjust the collider size and offset
        boxCollider.size = new Vector2(1.0f, 1.0f); // Adjust these values as needed
        boxCollider.offset = new Vector2(0.0f, 0.0f); // Adjust these values as needed
    }

    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                animator.SetFloat("move x", input.x);
                animator.SetFloat("move y", input.y);

                Vector3 targetPos = transform.position + new Vector3(input.x, input.y, 0);
                facingDir = new Vector3(input.x, input.y, 0);

                if (isWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }

        animator.SetBool("isMoving", isMoving);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }
    }

    private void Interact()
    {
        Vector3 interactPos = transform.position + facingDir;
        Debug.DrawLine(transform.position, interactPos, Color.red, 1f);

        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactableLayer);
        if (collider != null)
        {
            collider.GetComponent<Interact>()?.Interact();
        }
    }

    private IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }

    private bool isWalkable(Vector3 targetPos)
    {
        return Physics2D.OverlapCircle(targetPos, 0.1f, solidObjectLayer | interactableLayer) == null;
    }

    private void OnDrawGizmos()
    {
        if (facingDir != Vector3.zero)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + facingDir);
        }

        // Draw the walkable check circle
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + facingDir, 0.1f); // Use the same radius as in isWalkable
    }
}