using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{

    [Header("Amounts")]
    [SerializeField] private int woodAmount;
    [SerializeField] private bool detectingPlayer;
    [SerializeField] private float timeAmount;

    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteHouse;
    [SerializeField] private Transform point;
    [SerializeField] private GameObject houseColl;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;

    private PlayerAnim playerAnim;
    private Player player;
    private Playeritems playeritems;
    private float timeCount;
    private bool isBegining;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        playerAnim =  player.GetComponent<PlayerAnim>();
        playeritems = player.GetComponent<Playeritems>();
    }

    // Update is called once per frame
    void Update()
    {
        if (detectingPlayer && Input.GetKeyDown(KeyCode.E) && playeritems.totalWood >= woodAmount )
        {
            //construção é inicializada
            isBegining = true;
            playerAnim.OnHammeringStarted();
            spriteHouse.color = startColor;
            player.transform.position = point.position;
            player.isPaused = true;
            playeritems.totalWood -= woodAmount;
        }

        if (isBegining)
        {
            timeCount += Time.deltaTime;
            if(timeCount >= timeAmount)
            {
                //casa é finalizada
                playerAnim.OnHammeringEnded();
                spriteHouse.color = endColor;
                player.isPaused = false;
                houseColl.SetActive(true);
            }
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
