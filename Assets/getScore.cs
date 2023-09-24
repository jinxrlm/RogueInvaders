using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getScore : MonoBehaviour
{
    public IntVariable curScore;
    Text textscore;

    // Start is called before the first frame update
    void Start()
    {
        curScore.curValue = 0;
        textscore = GetComponent<Text>();
        print(textscore.text);
    }

    // Update is called once per frame
    void Update()
    {
        textscore.text = curScore.curValue.ToString();
    }
}
