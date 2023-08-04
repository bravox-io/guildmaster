using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public float DialogueRange;
    public LayerMask playerLayer;

    public DialogueSets dialogues;

    bool playerHit;

    private List<string> sentences = new List<string>();
    private List<string> actorName = new List<string>();
    private List<Sprite> actorSprite = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        GetNPCInfor();
    }


    //é chamado a cada frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && playerHit)
        {
            DialogueController.instance.speech(sentences.ToArray(), actorName.ToArray(),actorSprite.ToArray());
        }
    }

    void GetNPCInfor()
    {
        for (int i = 0; i < dialogues.dialogue.Count; i++)
        {
            switch (DialogueController.instance.language)
            {
                case DialogueController.idiom.pt:
                    sentences.Add(dialogues.dialogue[i].sentence.Portugues);
                    break;
                
                case DialogueController.idiom.eng:
                    sentences.Add(dialogues.dialogue[i].sentence.English);
                    break;
                
                case DialogueController.idiom.spa:
                    sentences.Add(dialogues.dialogue[i].sentence.Spanish);
                    break;
            }

            actorName.Add(dialogues.dialogue[i].actorName);
            actorSprite.Add(dialogues.dialogue[i].profile);

            
        }


    }

    

    //é usado pela fisica
    private void FixedUpdate()
    {
        ShowDialogue();
    }


    void ShowDialogue()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, DialogueRange, playerLayer);

        if (hit != null)
        {
           // Debug.Log("player area");
            playerHit = true;

        }
        else
        {
            playerHit = false;
           
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, DialogueRange); 
    }
}
