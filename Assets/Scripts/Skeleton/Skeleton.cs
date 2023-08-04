using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Skeleton : MonoBehaviour
{
    [Header("components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AnimationsControl AnimationsControl;

    [Header("Stats")]
    public Image healthBar;
    public float currentHealth;
    public float healthTotal;
    private Player player;
    private bool detectPlayer;
    public bool isDeath;
    public float radius;
    public LayerMask Layer;
   

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = healthTotal;
        player = FindObjectOfType<Player>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDeath && detectPlayer)
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);

            if (Vector2.Distance(transform.position, player.transform.position) <= agent.stoppingDistance)
            {
                // chegou no limite de distancia 
                AnimationsControl.PlayerAnim(2);


            }
            else
            {
                //skeleton segue o player
                AnimationsControl.PlayerAnim(1);
            }

            float posX = player.transform.position.x - transform.position.x;

            if (posX > 0)
            {
                transform.eulerAngles = new Vector2(0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 180);
            }

        }


    }

    private void FixedUpdate()
    {
        DetectPlayer();
    }

    public void DetectPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, Layer);

        if (hit != null)
        {
            // enxergou o player

            detectPlayer = true;

        }
        else
        {
            //não enxergou o player
            detectPlayer = false;
            AnimationsControl.PlayerAnim(0);
            agent.isStopped = true;

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
