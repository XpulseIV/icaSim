using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dialougemanager : MonoBehaviour
{
    public TextMeshProUGUI nametext;
    public TextMeshProUGUI dialoguetext; 


    private Queue<string> sentences;

    internal void StartDialogue(Dialouge dialogue)
    {
        sentences = new Queue<string>();
        
        nametext.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialoguetext.text = sentence;
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");
    }
}
