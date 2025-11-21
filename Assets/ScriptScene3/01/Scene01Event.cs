using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class DialogueElement
{
    public string speaker;
    [TextArea(2, 6)]
    public string text;
    public bool playVoice = true;
    public VoiceType voiceType;
    public GameObject momObject;
    public GameObject lantomObject;
    public float momActiveTime = 0f;
    public float lantomActiveTime = 0f;
}

public enum VoiceType
{
    Ah,
    Amm,
    Er,
    Um,
    None
}

public class Scene01Event : MonoBehaviour
{
    public GameObject fadeScreenIn;
    public GameObject mom;
    public GameObject lantom;
    public GameObject textbox;
    public AudioSource Ah;
    public AudioSource Amm;
    public AudioSource Er;
    public AudioSource Um;
    public GameObject nextButton;
    public int eventPos = 0;
    public List<DialogueElement> dialogueElements = new List<DialogueElement>();
    public TextMeshProUGUI textUI;
    public TextMeshProUGUI speakerUI;

    public string nextSceneName;

    private int currentTextIndex = 0;
    private bool firstNextDone = false;
    private bool isPaused = false;

    public GameObject choicePanel;
    public Button choiceButtonA;
    public Button choiceButtonB;
    public string choiceAScene;
    public string choiceBScene;

    void Start()
    {
        StartCoroutine(EventStarter());
    }

    IEnumerator EventStarter()
    {
        yield return new WaitForSeconds(2);
        fadeScreenIn.SetActive(false);
        eventPos = 1;
        textbox.SetActive(true);
        nextButton.SetActive(true);
        if (dialogueElements.Count > 0)
        {
            StartCoroutine(ShowDialogueCoroutine(dialogueElements[0]));
        }
    }

    IEnumerator EventOne()
    {
        nextButton.SetActive(false);
        textbox.SetActive(true);
        nextButton.SetActive(true);
        yield return new WaitForSeconds(2);
        lantom.SetActive(true);
    }

    public void NextButton()
    {
        if (isPaused) return;
        if (eventPos == 1)
        {
            if (!firstNextDone)
            {
                StartCoroutine(EventOne());
                firstNextDone = true;
            }
            else
            {
                if (currentTextIndex < dialogueElements.Count - 1)
                {
                    if (dialogueElements[currentTextIndex].momObject != null)
                        dialogueElements[currentTextIndex].momObject.SetActive(false);
                    if (dialogueElements[currentTextIndex].lantomObject != null)
                        dialogueElements[currentTextIndex].lantomObject.SetActive(false);

                    currentTextIndex++;
                    StartCoroutine(ShowDialogueCoroutine(dialogueElements[currentTextIndex]));
                }
                else
                {
                    if (dialogueElements[currentTextIndex].momObject != null)
                        dialogueElements[currentTextIndex].momObject.SetActive(false);
                    if (dialogueElements[currentTextIndex].lantomObject != null)
                        dialogueElements[currentTextIndex].lantomObject.SetActive(false);

                    ShowChoicePanel(choiceAScene, choiceBScene);
                }
            }
        }
    }

    IEnumerator ShowDialogueCoroutine(DialogueElement element)
    {
        if (element.momObject != null)
        {
            element.momObject.SetActive(true);
            if (element.momActiveTime > 0f)
                StartCoroutine(DisableAfterTime(element.momObject, element.momActiveTime));
        }
        if (element.lantomObject != null)
        {
            element.lantomObject.SetActive(true);
            if (element.lantomActiveTime > 0f)
                StartCoroutine(DisableAfterTime(element.lantomObject, element.lantomActiveTime));
        }

        textUI.text = element.text;
        speakerUI.text = element.speaker;

        if (element.playVoice)
        {
            switch (element.voiceType)
            {
                case VoiceType.Ah: Ah.Play(); break;
                case VoiceType.Amm: Amm.Play(); break;
                case VoiceType.Er: Er.Play(); break;
                case VoiceType.Um: Um.Play(); break;
            }
        }

        yield return null;
    }

    IEnumerator DisableAfterTime(GameObject obj, float time)
    {
        float elapsed = 0f;
        while (elapsed < time)
        {
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        if (obj != null)
            obj.SetActive(false);
    }

    // pause
    public void PauseButton()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f;
            AudioListener.pause = true;
        }
        else
        {
            Time.timeScale = 1f;
            AudioListener.pause = false;
        }
    }

    public void ShowChoicePanel(string sceneA, string sceneB)
    {
        isPaused = true;
        nextButton.SetActive(false);
        choicePanel.SetActive(true);

        choiceButtonA.onClick.RemoveAllListeners();
        choiceButtonB.onClick.RemoveAllListeners();

        choiceButtonA.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(sceneA);
        });

        choiceButtonB.onClick.RemoveAllListeners();
        choiceButtonB.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(sceneB);
        });
    }
}