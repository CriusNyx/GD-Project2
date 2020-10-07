using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGUI : MonoBehaviour
{
    StartingMessage startingMessages;
    List<ConversationGUI> conversations = new List<ConversationGUI>();
    ConversationGUI currentGui;
    List<string> complaints = new List<string>();
    float lastEscapeTime = -1;
    int salary = 25000;
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

    bool helpLock;
    string helpStatus = "";

    // Start is called before the first frame update
    void Start()
    {
        startingMessages = new StartingMessage();
        currentGui = new ConversationGUI(new Conversation(startingMessages.GetOne()));
        conversations.Add(currentGui);

        StartCoroutine(Complaint());
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

        foreach(var conversation in conversations)
        {
            conversation.Update();
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
                        if (GUILayout.Button($"Call {counter}", GUILayout.Width(70)))
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
                if (GUILayout.Button("Help"))
                {
                    salary -= 25;
                    StartCoroutine(RequestHelp());
                }
                GUILayout.Label(helpStatus);
                GUILayout.Label("Job Preformance:");
                foreach(var complaint in complaints)
                {
                    GUILayout.Label(complaint);
                }
            }
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Label("Salary: $" + salary);
            }
        }
    }

    

    private IEnumerator RequestHelp()
    {
        if (!helpLock)
        {
            helpLock = true;

            helpStatus = "You have requested for help from your boss. You will receive help soon. You have been fined $25 for asking for help.";
            yield return new WaitForSeconds(10);
            helpStatus = "";
            helpLock = false;
        }
    }

    private IEnumerator Complaint()
    {
        yield return new WaitForSeconds(30);
        complaints.Add("You have received a complaint from a customer. If you receive too many complaints, you will be considered for termination. Your salary has been deducted by $500.");
        salary -= 500;
        yield return new WaitForSeconds(12);
        complaints.Add("You have received a complaint from a customer. If you receive too many complaints, you will be considered for termination. Your salary has been deducted by $10000.");
        salary -= 10000;
        yield return new WaitForSeconds(30);
        complaints.Add("You have received a complaint from a customer. If you receive too many complaints, you will be considered for termination. Your salary has been deducted by $7500");
        salary -= 7500;
        yield return new WaitForSeconds(20);
        complaints.Add("You have received a complaint from a customer. If you receive too many complaints, you will be considered for termination. Your salary has been deducted by $200.");
        yield return new WaitForSeconds(40);
        salary -= 200;
        complaints.Add("You have received a complaint from a customer. If you receive too many complaints, you will be considered for termination. Your salary has been deducted by $2500.");
        salary -= 2500;
        yield return new WaitForSeconds(3);
        complaints.Add("You have received a complaint from a customer. If you receive too many complaints, you will be considered for termination. Your salary has been deducted by $999");
        salary -= 999;
        yield return new WaitForSeconds(10);
        complaints.Add("Your performance has dropped low to earn pay. You are now EA's official indentured servant, as per your contract.");
        salary = 0;
    }
}
