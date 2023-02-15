using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionDialogUI : MonoBehaviour
{
    internal static QuestionDialogUI Instance { get; private set; }

    private TextMeshProUGUI textMeshPro;
    private TextMeshProUGUI yesBtnText;
    private Button yesBtn;
    private Button noBtn;

    private GameObject yesBbtn;
    private GameObject noBbtn;

    private void Awake()
    {
        Instance = this;

        textMeshPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        yesBtn = transform.Find("YesBtn").GetComponent<Button>();
        yesBtnText = this.yesBtn.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        noBtn = transform.Find("NoBtn").GetComponent<Button>();

        this.yesBbtn = transform.Find("YesBtn").gameObject;
        this.noBbtn = transform.Find("NoBtn").gameObject;

        Hide();
    }

    public void ShowQuestion(string questionText, Action yesAction, Action noAction, bool showNo = false,
        string yesText = "Continue")
    {
        gameObject.SetActive(true);

        textMeshPro.text = questionText;
        this.yesBtnText.text = yesText;

        if (!showNo)
        {
            this.yesBbtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -35);
            this.noBbtn.SetActive(false);
        }

        yesBtn.onClick.AddListener(() =>
        {
            Hide();
            yesAction();
        });
        noBtn.onClick.AddListener(() =>
        {
            Hide();
            noAction();
        });
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}