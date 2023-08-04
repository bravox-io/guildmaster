using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarm : MonoBehaviour
{

    [Header("Sounds")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip holeSFX;
    [SerializeField] private AudioClip carrotSFX;


    [Header("Components")]
    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite carrot;
    [SerializeField] private SpriteRenderer spriteRenderer;


    [Header("Settings")]
    [SerializeField] private int digAmount; //quantidade de "escavação"
    [SerializeField] private float WaterAmount; //total de àgua para nascer uma cenoura
    [SerializeField] private bool detecting;
    private bool dugHole;
    private bool plantedCarrot;
    private bool isPlayer; // fica verdadeiro quando o player está encostando 
    private int initialDigAmount;
    private float currentWater;

    Playeritems playeritems;


    // Start is called before the first frame update
    void Start()
    {
        playeritems = FindObjectOfType<Playeritems>();
        initialDigAmount = digAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (dugHole)
        {
            if (detecting)
            {
                currentWater += 0.01f;
            }

            //encheu o total de água necessario
            if (currentWater >= WaterAmount && !plantedCarrot)
            {
                audioSource.PlayOneShot(holeSFX);
                spriteRenderer.sprite = carrot;

                plantedCarrot = true;
            }

            if (Input.GetKeyDown(KeyCode.E) && plantedCarrot && isPlayer)
            {
                audioSource.PlayOneShot(carrotSFX);
                spriteRenderer.sprite = hole;
                playeritems.carrots++;
                currentWater = 0f;
            }
        }
        
       
    }

    public void OnHit()
    {
        digAmount--;

        if (digAmount <= initialDigAmount / 2)
        {
            spriteRenderer.sprite = hole;
            dugHole = true;
         
        }

        //if (digAmount <= 0)
        //{
        //    //planta cenoura
        //    
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dig"))
        {
            OnHit();
            //Debug.Log("bateu");
        }

        if (collision.gameObject.CompareTag("Water"))
        {
            detecting = true;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            detecting = false;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayer = false;
        }
    }
}
