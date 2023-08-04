using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsControl : MonoBehaviour
{
    private Animator Anim;
    private PlayerAnim playerAnim;
    private Skeleton skeleton;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float radius;
    [SerializeField] private Transform attackPoint;

    private void Start()
    {
        Anim = GetComponent<Animator>();
        playerAnim = FindObjectOfType<PlayerAnim>();
        skeleton = GetComponentInParent<Skeleton>();
    }

    public void PlayerAnim(int value)
    {
        Anim.SetInteger("transition", value);
    }

    public void Attack()
    {
        if (!skeleton.isDeath)
        {
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, playerLayer);

            if (hit != null)
            {
                //detecta colisão com o player
                //Debug.Log("bateu");
                playerAnim.OnHit();

            }
        }

       
        
    }

    public void OnHit()
    {
        if(skeleton.currentHealth <= 0)
        {
            skeleton.isDeath = true;
            Anim.SetTrigger("isDeath");

            Destroy(skeleton.gameObject, 5f);
        }
        else
        {
            Anim.SetTrigger("isHurt");
            skeleton.currentHealth--;

            skeleton.healthBar.fillAmount = skeleton.currentHealth / skeleton.healthTotal;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}
