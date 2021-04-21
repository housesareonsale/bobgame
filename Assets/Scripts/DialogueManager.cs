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
    Queue<string> sentences;

    Coroutine currentCoroutine;
    bool continueEvent = false;
    void Start()
    {
        sentences = new Queue<string>();    
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if(!dialogBox.activeInHierarchy)
        {
            dialogBox.SetActive(true);
        }

        boxAnimator.SetTrigger("StartDialogue");
        nameBox.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        if(dialogue.shakeScreen)
        {
            shaker.Shake(0.8f);
        }

        continueEvent = true;
        DisplayNextSentence();        
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }


        string sentence = sentences.Dequeue();

        if(currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(TypeSentence(sentence));
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
