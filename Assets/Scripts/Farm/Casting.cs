using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casting : MonoBehaviour
{
    [SerializeField] private bool detectingPlayer;
    [SerializeField] private int percentage; //porcentagem de chance de pescar um peixe a cada tentativas
    [SerializeField] private GameObject fishPrefab;

    private Playeritems player;
    private PlayerAnim playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Playeritems>();  
        playerAnim = player.GetComponent<PlayerAnim>();
    }

    // Update is called once per frame
    void Update()
    {
        if (detectingPlayer && Input.GetKeyDown(KeyCode.E))
        {
            
            playerAnim.OnCastingStarted();
        }

    }

    public void OnCasting()
    {
        int randomValue = Random.Range(1, 100);

        if(randomValue <= percentage)
        {
            // conseguiu pescar um peixe
            Instantiate(fishPrefab,player.transform.position + new Vector3(Random.Range(-2.5f,-1f), 0f, 0f), Quaternion.identity);
            Debug.Log("pescou");
        }
        else
        {
            // Não conseguiu pescar nada
            Debug.Log("Não pescou");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            detectingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            detectingPlayer = false;
        }
    }
}
