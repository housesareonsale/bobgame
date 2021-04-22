using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameBox;
    public TextMeshProUGUI textBox;
    public Animator boxAnimator;
    public ScreenShake shaker;
    public GameController gameController;
    public GameObject dialogBox;
    Queue<Dialogue> dialogues;

    Coroutine currentCoroutine;
    bool continueEvent = false;
    void Start()
    {
        dialogues = new Queue<Dialogue>();    
    }

    public void StartDialogue(Dialogue[] conversation)
    {
        if(!dialogBox.activeInHierarchy)
        {
            dialogBox.SetActive(true);
        }

        boxAnimator.SetTrigger("StartDialogue");

        if(dialogues == null)
        {
            dialogues = new Queue<Dialogue>();
        }
        else
        {
            dialogues.Clear();
        }

        foreach(Dialogue dialogue in conversation)
        {
            dialogues.Enqueue(dialogue);
        }

        continueEvent = true;
        DisplayNextSentence();        
    }

    public void DisplayNextSentence()
    {
        if(dialogues.Count == 0)
        {
            EndDialogue();
            return;
        }


        Dialogue dialogue = dialogues.Dequeue();
        nameBox.text = dialogue.name;

        if(dialogue.shakeScreen)
        {
            shaker.Shake(0.8f);
        }

        if(currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(TypeSentence(dialogue.sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        textBox.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            textBox.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        boxAnimator.SetTrigger("EndDigaloue");

        if(continueEvent)
        {
            gameController.CotinueEvent();
            continueEvent = false;
        }
    }
}
