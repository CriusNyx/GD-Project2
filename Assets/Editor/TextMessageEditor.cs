using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(TextMessage))]
public class TextMessageEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TextMessage tm = target as TextMessage;

        EditorGUI.BeginChangeCheck();

        tm.startingMessage = EditorGUILayout.Toggle(
            new GUIContent("Start Message", "Indicates that this text message will start a new conversation"),
            tm.startingMessage);

        tm.endConversation = EditorGUILayout.Toggle(
            new GUIContent("End Conversation", "Indicates that this text message will end the conversation."),
            tm.endConversation);

        tm.message = EditorGUILayout.TextField(
            new GUIContent("Message", "The text message the NPC will send to the player"),
            tm.message);

        tm.responses = DrawArrayEditor(new GUIContent("Responses", "The options the player has to respond"), tm.responses, ResponseEditor);

        tm.timeout = EditorGUILayout.FloatField(
            new GUIContent("Timeout",
            "How much time the player has to respond to the message.\n"
            + "To make a dialogue progress automatically, make responses empty, and set a timeout to send the next message.\n"
            + "Set this to -1 to make a conversation not time out."),
            tm.timeout);

        tm.timeoutResponse = EditorGUILayout.ObjectField(
            new GUIContent(
                "Timeout Response", "The response that an NPC will give if the player waits too long."),
            tm.timeoutResponse,
            typeof(TextMessage),
            allowSceneObjects: false)
            as TextMessage;

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(target);
        }
    }

    private T[] DrawArrayEditor<T>(GUIContent label, T[] arr, Func<T, T> valueEditor)
    {
        if (arr == null)
        {
            arr = new T[0];
        }

        DrawHLine();
        GUILayout.Label(label);
        DrawHLine();

        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = valueEditor(arr[i]);
            if (GUILayout.Button("Remove"))
            {
                List<T> items = new List<T>(arr);
                items.RemoveAt(i);
                return items.ToArray();
            }
            DrawHLine();
        }

        DrawHLine();

        if (GUILayout.Button("Add Element"))
        {
            var newArr = new T[arr.Length + 1];
            Array.Copy(arr, 0, newArr, 0, arr.Length);
            arr = newArr;
        }

        DrawHLine();
        DrawHLine();

        return arr;
    }

    private static MessageResponse ResponseEditor(MessageResponse response)
    {
        if (response == null)
        {
            response = new MessageResponse();
        }
        response.message = EditorGUILayout.TextField(
            new GUIContent("Message", "An option the player can choose to respond with"),
            response.message);

        response.nextMessage = EditorGUILayout.ObjectField(
            new GUIContent(
                "Next Message", "The response that an NPC will give if the player uses this response."),
            response.nextMessage,
            typeof(TextMessage),
            allowSceneObjects: false)
            as TextMessage;

        response.push = EditorGUILayout.Toggle(
            new GUIContent("Push", "If checked this response will push the following message on the stack."),
            response.push);

        response.popMessage = EditorGUILayout.ObjectField(
            new GUIContent(
                "Pop Message", "If this response is marked as a push response, This message will display after a stack pop."),
            response.popMessage,
            typeof(TextMessage),
            allowSceneObjects: false)
            as TextMessage;

        response.pop = EditorGUILayout.Toggle(
            new GUIContent("Pop", "If this message is set to pop, it will pop the stack, returning control flow to the last set pop message."),
            response.pop);

        response.endConversation = EditorGUILayout.Toggle(
            new GUIContent("End Conversation", "If checked, the conversation will end after this option is selected."),
            response.endConversation);

        return response;
    }

    private void DrawHLine()
    {
        GUILayout.Box("", GUILayout.Height(5));
    }
}
