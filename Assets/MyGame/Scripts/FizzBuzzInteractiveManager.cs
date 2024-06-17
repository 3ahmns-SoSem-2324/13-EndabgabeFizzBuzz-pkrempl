using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class FizzBuzzInteractiveManager : MonoBehaviour
{
    public TextMeshProUGUI numberText;
    public TextMeshProUGUI fizzText;
    public TextMeshProUGUI hintText;
    public TextMeshProUGUI errorCountText;
    public Image panelImage;

    private int randomNumber;
    private bool correctKeyPressed = true;
    private bool wrongKeyPressed = false;
    private float delayTime = 1f;
    private const string FizzString = "Fizz";
    private const string BuzzString = "Buzz";
    private const string FizzBuzzString = "FizzBuzz";
    private const string NoFizzBuzzString = "Kein FizzBuzz";
    private const string CorrectFeedback = "Richtig!";
    private const string WrongFeedback = "Falsch!";
    private const string ContinueHint = "Falsche Antwort! Drücke Leertaste, um fortzufahren.";
    private const string DivisibilityRule3 = "Eine Zahl ist durch 3 teilbar, wenn ihre Quersumme (alle Ziffern zusammenzählen) durch 3 teilbar ist.";
    private const string DivisibilityRule5 = "Eine Zahl ist durch 5 teilbar, wenn die letzte Ziffer eine 0 oder 5 ist.";

    private int errorCount = 0;
    private const int MaxErrors = 10;

    void Start()
    {
        GenerateRandomNumber();
        panelImage.color = Color.black;
        errorCountText.text = "Fehler: 0";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (wrongKeyPressed)
            {
                hintText.text = "";
                wrongKeyPressed = false;
                correctKeyPressed = true;
                GenerateRandomNumber();
            }
        }
        else
        {
            if (correctKeyPressed)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    CheckFizzBuzz(FizzString);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                {
                    CheckFizzBuzz(BuzzString);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    CheckFizzBuzz(FizzBuzzString);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    CheckFizzBuzz(NoFizzBuzzString);
                }
            }
        }
    }

    public void CheckFizzBuzz(string choice)
    {
        string fizzBuzz = GetFizzBuzz(randomNumber);

        if (choice == fizzBuzz)
        {
            correctKeyPressed = true;
            panelImage.color = Color.green;
            fizzText.text = fizzBuzz == FizzString ? FizzString : fizzBuzz == BuzzString ? BuzzString : fizzBuzz == FizzBuzzString ? FizzBuzzString : "";
            StartCoroutine(GenerateRandomNumberWithDelay());
        }
        else
        {
            correctKeyPressed = false;
            wrongKeyPressed = true;
            panelImage.color = Color.red;
            hintText.text = ContinueHint;
            fizzText.text = "";

            if (choice == FizzString)
            {
                hintText.text += "\n" + DivisibilityRule3;
            }
            else if (choice == BuzzString)
            {
                hintText.text += "\n" + DivisibilityRule5;
            }
            else if (choice == FizzBuzzString)
            {
                hintText.text += "\n" + DivisibilityRule3 + "\n" + DivisibilityRule5;
            }

            errorCount++;
            errorCountText.text = "Fehler: " + errorCount; // Aktualisierung des Fehlerzählers

            if (errorCount >= MaxErrors)
            {
                SceneManager.LoadScene("GameOverScene"); // Assuming you have a scene named "GameOverScene"
            }
        }
    }

    IEnumerator GenerateRandomNumberWithDelay()
    {
        yield return new WaitForSeconds(delayTime);
        GenerateRandomNumber();
    }

    public void GenerateRandomNumber()
    {
        fizzText.text = "";
        hintText.text = "";
        randomNumber = Random.Range(1, 1000);
        numberText.text = randomNumber.ToString();
    }

    private string GetFizzBuzz(int number)
    {
        if (number % 3 == 0 && number % 5 == 0)
        {
            return FizzBuzzString;
        }
        else if (number % 3 == 0)
        {
            return FizzString;
        }
        else if (number % 5 == 0)
        {
            return BuzzString;
        }
        else
        {
            return NoFizzBuzzString;
        }
    }
}

