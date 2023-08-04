using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float speed;
    private float initialSpeed;

    private Animator anim;

    private int index;

    public List<Transform> paths = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        initialSpeed = speed;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(DialogueController.instance.isShowing == true)
        {
            speed = 0f;
            anim.SetBool("isWalking", false);
        }
        else
        {
            speed = initialSpeed;
            anim.SetBool("isWalking", true);
        }
        
        transform.position = Vector2.MoveTowards(transform.position, paths[index].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, paths[index].position) < 0.1f)
        {
            if (index < paths.Count - 1)
            {
                index++; // seguir os paths da ordem num�rica 
                //index = Random.Range(0, paths.Count -1); // deixar o NPC seguir os paths aleatoriamente 

            }
            else
            {
                index = 0;
            }
        }

        Vector2 direction = paths[index].position - transform.position;


        if (direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        if(direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }
}
