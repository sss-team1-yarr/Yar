using System;
using System.Collections;
using _03_Code.Player.Components;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float detectRange = 5f;
    [SerializeField] private float knockBackForce = 20f;
    [SerializeField] private float knockBackTime = 0.1f;

    private float direction;
    private bool isKnockedBack = false;
    
    private void Reset()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isKnockedBack) return;
        
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > detectRange)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        direction = player.position.x > transform.position.x ? 1f : -1f;

        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
    }
    
    public void KnockBack()
    {
        if (isKnockedBack) return;
        StartCoroutine(KnockBackRoutine());
    }

    private IEnumerator KnockBackRoutine() {
        isKnockedBack = true;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(new Vector2(knockBackForce * -direction, 0f), ForceMode2D.Impulse);
        yield return new WaitForSeconds(knockBackTime);
        isKnockedBack = false;
    }
}

