using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextAnimationControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator textAnim;
    public TextMeshProUGUI textString;
    public void TextSlide(string _text)
    {
        textString.text = _text;
        textAnim.SetTrigger("Start");
    }
    public void TextAppearence(string _text)
    {
        textString.text = _text;
        textAnim.SetTrigger("End");
    }
}
