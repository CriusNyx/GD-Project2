using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Specifies a single text message from an NPC
/// </summary>
[CreateAssetMenu(fileName = "TextMessage", menuName = "TextMessage")]
public class TextMessage : ScriptableObject
{
    /// <summary>
    /// If set, this will start a conversation between the player and an NPC.
    /// </summary>
    public bool startingMessage;

    /// <summary>
    /// If set, this conversation will end when this message is displayed.
    /// </summary>
    public bool endConversation;

    /// <summary>
    /// The text the NPC will send to the player
    /// </summary>
    public string message;

    /// <summary>
    /// The messages the player can choose to respond with.
    /// </summary>
    public MessageResponse[] responses;

    /// <summary>
    /// How much time the player has to respond to the message.
    /// To make a dialogue progress automatically, make responses empty, and set a timeout to send the next message.
    /// Set this to -1 to make a conversation not time out.
    /// </summary>
    public float timeout;

    /// <summary>
    /// The message an NPM will send after the timeout time has been reached.
    /// </summary>
    public TextMessage timeoutResponse;
}
