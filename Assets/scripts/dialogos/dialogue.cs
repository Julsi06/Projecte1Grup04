using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dialogue : MonoBehaviour
{
    private bool IsPlayerInRange;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject dialogueTextObject; // GameObject que contiene el texto
    [SerializeField] private TMP_Text dialogueText; // Componente de texto
    [SerializeField, TextArea(2, 6)] private string[] dialogueLines;
    private int currentLineIndex = 0;
    bool isDialogueActive = false;
    [SerializeField] private float typingTime = 0.05f;
    private bool isTyping = false;

    void Start()
    {
        dialogueBox.SetActive(false);
        dialogueTextObject.SetActive(false);
    }

    void Update()
    {
        if (IsPlayerInRange && !isDialogueActive)
        {
            StartDialogue();
        }

        if (isDialogueActive && !isTyping && Input.GetKeyDown(KeyCode.E))
        {
            NextLine();
        }
    }

    private void StartDialogue()
    {
        isDialogueActive = true;
        dialogueBox.SetActive(true);
        dialogueTextObject.SetActive(true);
        currentLineIndex = 0;
        StartCoroutine(ShowLine());
    }

    private void NextLine()
    {
        currentLineIndex++;
        if (currentLineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        dialogueBox.SetActive(false);
        dialogueTextObject.SetActive(false);
        currentLineIndex = 0;
    }

    private IEnumerator ShowLine()
    {
        isTyping = true;
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[currentLineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }

        isTyping = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerInRange = false;
            if (isDialogueActive)
            {
                StopAllCoroutines(); // Detener escritura si está en progreso
                EndDialogue();
            }
        }
    }
}