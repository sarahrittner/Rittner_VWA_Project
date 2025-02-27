using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Druckplatte_Controller : MonoBehaviour
{
    public GameObject target;
    private bool isVisible = true;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("OnTriggerEnter2D called with collider: " + collider.name);
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger");
            ToggleVisibility();
        }
    }

    private void ToggleVisibility()
    {
        if (target != null)
        {
            isVisible = !isVisible;
            SpriteRenderer spriteRenderer = target.GetComponent<SpriteRenderer>();
            Collider2D collider2D = target.GetComponent<Collider2D>();

            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = isVisible;
            }
            else
            {
                Debug.LogWarning("Target does not have a SpriteRenderer component.");
            }

            if (collider2D != null)
            {
                collider2D.enabled = isVisible;
            }
            else
            {
                Debug.LogWarning("Target does not have a Collider2D component.");
            }

            Debug.Log("Toggled visibility to: " + isVisible);
        }
        else
        {
            Debug.LogWarning("Target is not assigned.");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("Target is not assigned in the Inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
