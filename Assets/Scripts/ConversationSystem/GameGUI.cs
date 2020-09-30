using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGUI : MonoBehaviour
{
    StartingMessage startingMessages;
    ConversationGUI gui;

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
    }

    private void OnGUI()
    {
        gui.Draw();
    }
}
