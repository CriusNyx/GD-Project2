using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGUI : MonoBehaviour
{
    StartingMessage startingMessages;
    List<ConversationGUI> conversations = new List<ConversationGUI>();
    ConversationGUI currentGui;
    float lastEscapeTime = -1;

    string companyPolicy =
@"Company Policy:

1. The customer is always right.
2. All employees must uphold the values of EA.
	- Quality
	- Honesty
	- Transparency
3. EA does not issue refunds for their live services under any circumstances. Customers should be expected to make responsible purchases.
4. EA does not issue refunds for recurrent user spending purchases under any circumstances. Customers should be expected to make responsible purchases.
5. EA is not legally liable for the consequences any of our products for any of our customers for any reason.
6. Customers must understand and accept this policy to use any of EA's products.
7. Employees must understand and accept this policy to continue employment at EA.

How to handle different customer situations.

1. If you do not have enough information to solve the problem, please ask the customer for more information.
2. If the customer is dissatisfied, please reassure the customer that you are there to help.
3. If the customer has requested that you perform an action that violates EA's company policy, please politely ensure that the customer understands EA's policy.
4. If the customer expresses that they do not understand EA's policy, please politely inform them.
5. If a customer expresses disagreement with the policy, gently remind the customer that they have already agreed to the policy, and that they are bound by contract.";

    // Start is called before the first frame update
    void Start()
    {
        startingMessages = new StartingMessage();
        currentGui = new ConversationGUI(new Conversation(startingMessages.GetOne()));
        conversations.Add(currentGui);
    }

    // Update is called once per frame
    void Update()
    {
        currentGui?.ProcessInput();

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

    int fixedFrameCount = 0;

    private void FixedUpdate()
    {
        if(fixedFrameCount % 120 == 0)
        {
            if(Random.value > 0.5f)
            {
                if (!startingMessages.IsEmpty)
                {
                    conversations.Add(new ConversationGUI(new Conversation(startingMessages.GetOne())));
                }
            }
        }

        fixedFrameCount++;
    }

    private void OnGUI()
    {
        using (new GUILayout.HorizontalScope())
        {
            using (new GUILayout.VerticalScope())
            {
                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Quit Job"))
                    {
                        Application.Quit();
                    }
                    int counter = 1;
                    foreach (var conversation in conversations)
                    {
                        if (GUILayout.Button($"Conversation {counter}"))
                        {
                            currentGui = conversation;
                        }
                        counter++;
                    }
                }

                currentGui?.Draw();
            }
            using (new GUILayout.VerticalScope())
            {
                GUILayout.Label(companyPolicy);
            }
        }
    }
}
