using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SessionControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnConnectionSuccess(int SessionID)
    {
        var displayField = GetComponent<TextMeshProUGUI>();
        if (SessionID < 0)
        {
            displayField.text = $"Logging locally (Session {SessionID})";
        }
        else
        {
            displayField.text = $"Connected to Server ( Session {SessionID})";
        }
    }

    public void OnConnectionFail(string ErrorMessage)
    {
        var displayField = GetComponent<TextMeshProUGUI>();
        displayField.text = $"Error: {ErrorMessage}";
    }
}
