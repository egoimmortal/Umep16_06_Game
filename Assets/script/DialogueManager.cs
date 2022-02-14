using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text TalkText;
    public Animator animator;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) //first talk
    {
        TotalStatic.bTalking = true;
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    public void SecondDialogue(Dialogue dialogue) //second talk
    {
        TotalStatic.bTalking = true;
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void MissionCompleteDialogue(Dialogue dialogue) //MissionComplete talk
    {
        TotalStatic.bTalking = true;
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }



    public void DisplayNextSentence()//下一句
    {
        TotalStatic.bTalking = true;
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)//打字效果
    {
        TalkText.text = "";
        foreach (char word in sentence.ToCharArray())
        {
            TalkText.text += word;
            yield return null;
        }
    }

    void EndDialogue()
    {
        TotalStatic.bTalking = false;
        animator.SetBool("IsOpen", false);
        NPCtest.Instance().MissionGet();
    }
}
