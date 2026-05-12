using System;
using System.Collections;
using _03_Code;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialText : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    [SerializeField] private GameObject wall;
    
    private int _tutorialCounter = 0;
    
    private void Reset()
    {
        text = GetComponent<TextMeshPro>();
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
            if (Keyboard.current.leftArrowKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame)
                StartCoroutine(FirstTutorial());
        
        if (_tutorialCounter == 1)
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
                StartCoroutine(SecondTutorial());
        
        if (_tutorialCounter == 2)
            if (Keyboard.current.zKey.wasPressedThisFrame)
                StartCoroutine(ThirdTutorial());
        
        if (_tutorialCounter == 3)
            if (Keyboard.current.cKey.wasPressedThisFrame) 
                StartCoroutine(TutorialEnd());        
    }

    private IEnumerator FirstTutorial()
    {
        yield return new WaitForSeconds(0.8f);
        text.SetText("Space : Jump");
        _tutorialCounter = 1;
    }
    
    private IEnumerator SecondTutorial()
    {
        yield return new WaitForSeconds(0.8f);
        text.SetText("Z : Attack");
        _tutorialCounter = 2;
    }
    
    private IEnumerator ThirdTutorial()
    {
        yield return new WaitForSeconds(0.8f);
        text.SetText("C : Dash");
        _tutorialCounter = 3;
    }
    
    private IEnumerator TutorialEnd()
    {
        yield return new WaitForSeconds(0.8f);
        wall.SetActive(false);
        gameObject.SetActive(false);
    }
}
