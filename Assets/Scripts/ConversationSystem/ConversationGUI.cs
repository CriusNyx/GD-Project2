using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationGUI
{
    private string response;
    Conversation conversation;

    public ConversationGUI(Conversation conversation)
    {
        this.conversation = conversation;
    }

    public void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (int.TryParse(response, out var selection))
            {
                // Make it 0 indexed
                var actualSelection = selection - 1;

                if (conversation.SetResponse(actualSelection))
                {
                    response = "";
                }
            }
        }
    }

    public void Draw()
    {
        response = GUILayout.TextField(response);
        foreach(var message in conversation.GetConversation())
        {
            GUILayout.Label(message);
        }

        var responses = conversation.GetResponses();

        for(int i = 0; i < responses.Length; i++)
        {
            var response = responses[i];
            GUILayout.Label((i + 1) + ": " + response);
        }
    }
}