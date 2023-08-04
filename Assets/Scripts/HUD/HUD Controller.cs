using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [Header("Items")]
    [SerializeField] private Image waterUIBar;
    [SerializeField] private Image woodUIBar;
    [SerializeField] private Image carrotUIBar;
    [SerializeField] private Image FishUIBar;

    [Header("Tools")]
    //[SerializeField] private Image axeUI;
    //[SerializeField] private Image shovelUI;
    //[SerializeField] private Image bucketUI;

    public List<Image> toolsUI = new List<Image>();
    
    [SerializeField] private Color selectedColor; 
    [SerializeField] private Color alphaColor;


    private Playeritems playeritems;
    private Player player;


    private void Awake()
    {
        playeritems = FindObjectOfType<Playeritems>();
        player = playeritems.GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        waterUIBar.fillAmount = 0f;
        woodUIBar.fillAmount = 0f;
        carrotUIBar.fillAmount = 0f;
        FishUIBar.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        waterUIBar.fillAmount = playeritems.currentWater / playeritems.waterLimit; 
        woodUIBar.fillAmount = playeritems.totalWood / playeritems.woodLimit;  
        carrotUIBar.fillAmount = playeritems.carrots / playeritems.CarrotLimit;
        FishUIBar.fillAmount = playeritems.fishes / playeritems.fishesLimited;


        //toolsUI[player.handlingOBJ].color = selectedColor;

        for (int i = 0; i < toolsUI.Count; i++)
        {
            if (i == player.handlingOBJ)
            {
                toolsUI[i].color = selectedColor;
            }
            else
            {
                toolsUI[i].color = alphaColor;
            }
        }
    }  
}
