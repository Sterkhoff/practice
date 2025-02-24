using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI DialogueText;
    public string[] lines;
    public float PrintingDelay;

    public int lineIndex;

    public void StartDialogue()
    {
        gameObject.SetActive(true);
        DialogueText.maxVisibleCharacters = 0;
        DialogueText.text = lines[0];
        lineIndex = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        for (var i = 0; i < lines[lineIndex].Length; i++)
        {
            DialogueText.maxVisibleCharacters++;
            yield return new WaitForSeconds(PrintingDelay);
        }
    }

    public void ContinueDialogue()
    {
        if (DialogueText.maxVisibleCharacters == lines[lineIndex].Length)
            TypeNextLine();
        else
        {
            DialogueText.maxVisibleCharacters = lines[lineIndex].Length;
            StopAllCoroutines();
        }
    }

    public void ChangeDialogLines(string[] newLines)
    {
        lineIndex = 0;
        lines = newLines;
    }

    private void EndDialogue() => gameObject.SetActive(false);

    private void TypeNextLine()
    {
        lineIndex++;
        if (lineIndex > lines.Length - 1)
            EndDialogue();
        else
        {
            DialogueText.text = lines[lineIndex];
            DialogueText.maxVisibleCharacters = 0;
            StartCoroutine(TypeLine());
        }
    }
}
