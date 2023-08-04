using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLayer;
 


    private Player player;
    private Animator anim;
    private Casting cast;

    private bool isHitting;

    private float timeCount;
    private float recoveryTime = 1.7f;
   

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
        
        cast = FindObjectOfType<Casting>();
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
        OnRun();

        timeCount += Time.deltaTime;

        if (isHitting)
        {
            if (timeCount >= recoveryTime)
            {
                isHitting = false;
                timeCount = 0f;
            }
        }
      

    }



    #region Movement
    void OnMove()
    {
        if (player.direction.sqrMagnitude > 0)
        {
            if (player.isRolling)
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("roll"))
                {
                    anim.SetTrigger("isRoll");
                }
              
            }
            else
            {
                anim.SetInteger("transition", 1);
            }
            
        }
        else
        {
            anim.SetInteger("transition", 0);
        }

        if (player.direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (player.direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }

        if (player.isCutting)
        {
            anim.SetInteger("transition", 3);
        }

        if (player.isDigging)
        {
            anim.SetInteger("transition", 4);
        }
        
        if (player.isWatering)
        {
            anim.SetInteger("transition", 5);
        }
    }

    void OnRun()
    {
        if (player.isRunning && player.direction.sqrMagnitude > 0)
        {
            anim.SetInteger("transition", 2); 
        }
    }

    #endregion

    #region Attack

    public void OnAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, enemyLayer);

        if(hit != null)
        {
            //atacou o inimigo
            hit.GetComponentInChildren<AnimationsControl>().OnHit();
            //Debug.Log("Acertou o Enemy");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }



    #endregion


    // é chamado quando o jogador pressiona o botã o de ação na lagoa
    public void OnCastingStarted()
    {
        anim.SetTrigger("isCasting");
        player.isPaused = true;
    }


    //é chamado quando termina de executar a animação de pescaria  
    public void OnCastingEnded()
    {
        cast.OnCasting();
        player.isPaused = false;
    }
     
    public void OnHammeringStarted()
    {
        anim.SetBool("isHammering", true);
    }
    
    public void OnHammeringEnded()
    {
        anim.SetBool("isHammering", false);
    }

    public void OnHit()
    {
       
        if (!isHitting)
        {
            anim.SetTrigger("isHit");
            isHitting = true;
        }
    }
   
}
