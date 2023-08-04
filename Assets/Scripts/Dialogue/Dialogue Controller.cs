using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{

    [System.Serializable]
    public enum idiom
    {
        pt,
        eng,
        spa
    }

    public idiom language;



    [Header("Components")]
    public GameObject dialogueOBJ; //janela do dialogo
    public Image profileSprite; //sprite do perfil
    public Text speechText; //texto da fala 
    public Text actorNameText; //nome do NPC


    [Header("Settings")]
    public float typingSpeed; //velocidade da fala


    //Variáveis de contole
    public bool isShowing; // se a janela está visível
    private int index; //index das sentenças
    private string[] sentences;
    private string[] actorNameCurrent;
    private Sprite[] actorSpriteCurrent;

    private Player player;


    public static DialogueController instance;


    //awake é chamado  antes de todos os start() na hierarquia de execução de scripts 
    private void Awake()
    {
        instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

 
    IEnumerator TypeSentences()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }


    //pula para próxima fala/frase 
    public void NextSentences()
    {
        if (speechText.text == sentences[index])
        {
            if(index < sentences.Length - 1)
            {
                index++;
                profileSprite.sprite = actorSpriteCurrent[index];
                actorNameText.text = actorNameCurrent[index];
                speechText.text = "";
                StartCoroutine(TypeSentences());
            }
            else // quando terminam os textos
            {
                speechText.text = "";
                actorNameText.text = "";

                index = 0;
                dialogueOBJ.SetActive(false);
                sentences = null;
                isShowing = false;
                player.isPaused = false;
            }
        }
    }
   
    //chama a fala do NPC
    public void speech(string[] text, string[] actorName, Sprite[] actorProfile)
    {
        if (!isShowing)
        {
            dialogueOBJ.SetActive(true);
            sentences = text;
            actorNameCurrent = actorName;
            actorSpriteCurrent = actorProfile;
            StartCoroutine(TypeSentences());
            isShowing = true;
            player.isPaused = true;

        }
    }
}
