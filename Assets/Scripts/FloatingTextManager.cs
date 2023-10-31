using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefabs;

    private List<FloatingText> floatingTexts = new List<FloatingText>();

    private void Update()
    {
        foreach (FloatingText txt in floatingTexts)
        {
            txt.UpdateFloatingText();
        }
    }
    public void Show(string msg, int fontSize, Color color, Vector3 pos, Vector3 motion, float duration)
    {
        FloatingText floatingText = GetFloatingText();

        floatingText.txt.text = msg;
        floatingText.txt.fontSize = fontSize;
        floatingText.txt.color = color;
        floatingText.obj.transform.position = Camera.main.WorldToScreenPoint(pos);
        floatingText.motion = motion;
        floatingText.duration = duration;

        floatingText.ShowT();
    }
    private FloatingText GetFloatingText()
    {
        FloatingText ftxt = floatingTexts.Find(t => !t.active);

        if (ftxt == null)
        {
            ftxt = new FloatingText();
            ftxt.obj = Instantiate(textPrefabs);
            ftxt.obj.transform.SetParent(textContainer.transform);
            ftxt.txt = ftxt.obj.GetComponent<Text>();

            floatingTexts.Add(ftxt);
        }
        return ftxt;
    }
}
