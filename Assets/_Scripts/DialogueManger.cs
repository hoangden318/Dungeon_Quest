using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManger : MonoBehaviour
{
    protected static DialogueManger instance;
    public static DialogueManger Instance => instance;

    public event Action OnDialogueEnd;
    private Queue<string> sentences;

    public Text nameText;
    public Text dialogueText;
    public Animator anim;
    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        sentences = new Queue<string>();
        
    }

    public void StartDialogueGuide(Dialogue dialogue)
    {
        
        anim.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
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
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = " ";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }
    public void EndDialogue()
    {
        
        anim.SetBool("IsOpen", false);
        AbilitySystem.Instance.SetActive();
        OnDialogueEnd?.Invoke();
    }
}
