using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Mafs : MonoBehaviour
{
    [SerializeField] private ChallengeTrigger challengeScript;
    [SerializeField] private int maxNumber;

    [SerializeField] private TMP_Text equation;
    [SerializeField] private Button answer1;
    [SerializeField] private Button answer2;
    [SerializeField] private Button answer3;

    private List<Button> answers;

    private List<string> operations = new List<string>() { "+", "-", "*" };
    private int a, b, c;
    private int d, e;
    private int indexOfCorrectAnswer;
    private string operation;

    void Start()
    {
        generateEquation();
        setAnswers();
        Debug.Log($"{a}{operation}{b}={c}, {d}, {e}");
        labelStuff();
    }

    void generateEquation()
    {
        operation = operations[Random.Range(0, operations.Count)];
        
        a = Random.Range(0, maxNumber);
        b = Random.Range(0, maxNumber);

        switch (operation)
        {
            case "+":
                c = a + b;
                break;
            case "-":
                c = a - b;
                break;
            case "*":
                c = a * b;
                break;
        }
    }

    void setAnswers()
    {
        do
        {
            d = Random.Range(0, maxNumber * 2);
            e = Random.Range(0, maxNumber * 2);
        } while (d == e || d == c || e == c);
        indexOfCorrectAnswer = Random.Range(0, 3);
    }

    void labelStuff()
    {
        // Equation
        equation.text = $"{a} {operation} {b} = ?";

        // Answers
        answers = new List<Button>() { answer1, answer2, answer3 };
        
        // Correct
        answers[indexOfCorrectAnswer].transform.GetChild(0).GetComponent<TMP_Text>().text = c.ToString();
        answers[indexOfCorrectAnswer].onClick.AddListener(() => {
            challengeScript.Fulfill();
        });
        answers.RemoveAt(indexOfCorrectAnswer);

        // Bullshit ones
        answers[0].transform.GetChild(0).GetComponent<TMP_Text>().text = d.ToString();
        answers[1].transform.GetChild(0).GetComponent<TMP_Text>().text = e.ToString();

        for (int i = 0; i < answers.Count; i++)
        {
            answers[i].onClick.AddListener(() =>
            {
                challengeScript.Fail();
            });
        }
    }
}
