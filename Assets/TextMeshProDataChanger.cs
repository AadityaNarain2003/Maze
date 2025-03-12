using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMeshProDataChanger : MonoBehaviour
{

    public TMP_Text displayText;

    // Start is called before the first frame update
    void Start()
    {
         displayText.text = PlayerPrefs.GetString("TotalTime");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
