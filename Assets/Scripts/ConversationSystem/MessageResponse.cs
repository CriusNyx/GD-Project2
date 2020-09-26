using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A message that the player can respond with to a customer
/// </summary>
[System.Serializable]
public class MessageResponse
{
    /// <summary>
    /// The message a player can choose to respond with
    /// </summary>
    public string message;

    /// <summary>
    /// The next message that the NPC will say after the players response
    /// </summary>
    public TextMessage nextMessage;

    /// <summary>
    /// If this is set, the player will end the conversation after sending this message.
    /// </summary>
    public bool endConversation;
}