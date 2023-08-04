using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private float treeHealth;
    [SerializeField] Animator anim;
    [SerializeField] GameObject woodPrefab;
    [SerializeField] private int totaWood; 
    [SerializeField] private ParticleSystem leafs;

    private bool isCut;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void OnHit()
    {
        treeHealth--;
        anim.SetTrigger("isHit");
        leafs.Play();

        if(treeHealth <= 0)
        {
            //cria o toco e instancia os drops (madeira) 
            for (int i = 0; i < totaWood; i++)
            {
                Instantiate(woodPrefab, transform.position + new Vector3(Random.Range(-1f, 1f),
                Random.Range(-1f, 1f), 0f), transform.rotation);
            }
            
             
            anim.SetTrigger("cut");

            isCut = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Axe") && !isCut)
        {
            OnHit();
            //Debug.Log("bateu");
        }

     
    }
}
