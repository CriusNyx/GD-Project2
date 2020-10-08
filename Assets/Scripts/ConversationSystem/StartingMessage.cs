using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StartingMessage
{
    private List<TextMessage> startingMessages;

    public StartingMessage()
    {
        startingMessages 
            = Resources.LoadAll("NPCs", typeof(TextMessage))
            .Select(x => x as TextMessage)
            .Where(x => x.startingMessage)
            .ToList();
    }

    public bool IsEmpty => startingMessages.Count == 0;

    public TextMessage GetOne()
    {
        int outputNum = Random.Range(0, startingMessages.Count);
        var output = startingMessages[outputNum];
        startingMessages.RemoveAt(outputNum);
        return output;
    }
}
