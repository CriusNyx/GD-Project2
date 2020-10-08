using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Conversation
{
    public List<string> lines = new List<string>();
    private TextMessage currentMessage;
    public float? timeToTimeout;

    public Conversation(TextMessage message)
    {
        SetCurrentMessage(message);
    }

    public string[] GetConversation()
    {
        return lines.ToArray();
    }

    public string[] GetResponses()
    {
        var output = currentMessage?.responses?.Select(x => x.message).ToArray();
        if (output != null)
        {
            return output;
        }
        else
        {
            return new string[] { };
        }
    }

    private void SetCurrentMessage(TextMessage message)
    {
        if (message != null)
        {
            lines.Add(message.message);

            if (message.timeout > 0f)
            {
                timeToTimeout = Time.time + message.timeout;
            }
            else
            {
                timeToTimeout = null;
            }
        }
        else
        {
            timeToTimeout = null;
        }
        this.currentMessage = message;
    }

    public bool SetResponse(int number)
    {
        if (0 <= number && number < currentMessage.responses.Length)
        {
            var response = currentMessage.responses[number];
            lines.Add(response.message);
            SetCurrentMessage(response.nextMessage);
            return true;
        }
        return false;
    }

    public void Update()
    {
        if (timeToTimeout != null && Time.time > timeToTimeout)
        {
            SetCurrentMessage(currentMessage.timeoutResponse);
        }
    }
}
