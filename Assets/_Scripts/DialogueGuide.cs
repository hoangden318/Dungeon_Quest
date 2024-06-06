using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueGuide : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject task;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fighter") && other.gameObject.name == "Player")
            DialogueManger.Instance.StartDialogueGuide(dialogue);
            task.GetComponent<BoxCollider2D>().enabled = false;
    }
}
