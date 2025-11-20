using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
	[Header("UI")]
	public TextMeshProUGUI speakerNameText;
	public TextMeshProUGUI dialogueText;
	public GameObject dialoguePanel;

	[Header("Settings")]
	public float typingSpeed = 0.03f;

	private Queue<string> sentences;
	private bool isTyping = false;
	private string currentSentence;

	void Start()
	{
		sentences = new Queue<string>();
		dialoguePanel.SetActive(false);
	}

	public void StartDialogue(string speakerName, List<string> lines)
	{
		dialoguePanel.SetActive(true);
		speakerNameText.text = speakerName;

		sentences.Clear();

		foreach (string line in lines)
		{
			sentences.Enqueue(line);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if (isTyping)
		{
			// Skip typing
			StopAllCoroutines();
			dialogueText.text = currentSentence;
			isTyping = false;
			return;
		}

		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		currentSentence = sentences.Dequeue();
		StartCoroutine(TypeSentence(currentSentence));
	}

	IEnumerator TypeSentence(string sentence)
	{
		isTyping = true;
		dialogueText.text = "";

		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(typingSpeed);
		}

		isTyping = false;
	}

	void EndDialogue()
	{
		dialoguePanel.SetActive(false);
	}
}
