using System;
using System.Collections;
using _03_Code;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialText : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private TextMeshPro text;
    [SerializeField] private GameObject wall;

    [Header("Images")] 
    [SerializeField] private GameObject firstImage1;
    [SerializeField] private GameObject firstImage2;
    [SerializeField] private GameObject secondImage;
    [SerializeField] private GameObject thirdImage;
    [SerializeField] private GameObject fourthImage;
    [SerializeField] private GameObject lastImage;
    
    private int _tutorialCounter = 0;
    
    private void Reset()
    {
        text = GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        secondImage.SetActive(false);
        thirdImage.SetActive(false);
        fourthImage.SetActive(false);
        lastImage.SetActive(false);
    }

    private void Update()
    {
        Tutorial();
    }

    private void FixedUpdate()
    {
        transform.position = new Vector2(GameManager.Instance.player.transform.position.x, GameManager.Instance.player.transform.position.y + 3f);
    }

    private void Tutorial()
    {
        if (_tutorialCounter == 0)
            if (Keyboard.current.leftArrowKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame 
                || Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame)
                StartCoroutine(FirstTutorial());
        
        if (_tutorialCounter == 1)
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
                StartCoroutine(SecondTutorial());
        
        if (_tutorialCounter == 2)
            if (Keyboard.current.shiftKey.wasPressedThisFrame)
                StartCoroutine(ThirdTutorial());
        
        if (_tutorialCounter == 3)
            if (Keyboard.current.zKey.wasPressedThisFrame)
                StartCoroutine(FourthTutorial());
        
        if (_tutorialCounter == 4)
            if (Keyboard.current.cKey.wasPressedThisFrame) 
                StartCoroutine(TutorialEnd());        
    }

    private IEnumerator FirstTutorial()
    {
        yield return new WaitForSeconds(0.8f);
        text.SetText("        : Jump");
        _tutorialCounter = 1;
        firstImage1.SetActive(false);
        firstImage2.SetActive(false);
        secondImage.SetActive(true);
    }
    
    private IEnumerator SecondTutorial()
    {
        yield return new WaitForSeconds(0.8f);
        text.SetText("      : Run");
        _tutorialCounter = 2;  
        secondImage.SetActive(false);
        thirdImage.SetActive(true);
    }
    
    private IEnumerator ThirdTutorial()
    {
        yield return new WaitForSeconds(0.8f);
        text.SetText("    : Attack");
        _tutorialCounter = 3;
        thirdImage.SetActive(false);
        fourthImage.SetActive(true);
    }
    
    private IEnumerator FourthTutorial()
    {
        yield return new WaitForSeconds(0.8f);
        text.SetText("    : Dash");
        _tutorialCounter = 4;
        fourthImage.SetActive(false);
        lastImage.SetActive(true);
    }
    
    private IEnumerator TutorialEnd()
    {
        yield return new WaitForSeconds(0.8f);
        wall.SetActive(false);
        gameObject.SetActive(false);
    }
}
