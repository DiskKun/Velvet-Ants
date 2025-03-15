using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SessionDisplay : MonoBehaviour
{
    public void OnConnectionSuccess(int sessionID)
    {
        var displayField = GetComponent<TextMeshProUGUI>();
        if (sessionID < 0)
        {
            displayField.text = $"Logging locally (Session {sessionID})";
        } else
        {
            displayField.text = $"Connected to server (Session {sessionID})";
        }
    }

    public void OnConnectionFail(string errorMessage)
    {
        var displayField = GetComponent<TextMeshProUGUI>();
        displayField.text = $"Error: {errorMessage})";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
