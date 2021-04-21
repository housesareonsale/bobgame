using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue[] conversation;
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(conversation);
    }
}
