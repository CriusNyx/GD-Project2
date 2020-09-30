using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGUI : MonoBehaviour
{
    StartingMessage startingMessages;
    ConversationGUI gui;
    float lastEscapeTime = -1;

    // Start is called before the first frame update
    void Start()
    {
        startingMessages = new StartingMessage();
        gui = new ConversationGUI(new Conversation(startingMessages.GetOne()));
    }

    // Update is called once per frame
    void Update()
    {
        gui.ProcessInput();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.time - lastEscapeTime < 1f)
            {
                Application.Quit();
                Debug.Log("Quit");
            }
            else
            {
                lastEscapeTime = Time.time;
            }
        }
    }

    private void OnGUI()
    {
        using (new GUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Quit Job"))
            {
                Application.Quit();
            }
        }

        gui.Draw();
    }
}
