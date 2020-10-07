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
<<<<<<< Updated upstream
        gui.Draw();
=======
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
                if (GUILayout.Button("Help"))
                {
                    salary -= 25;
                    StartCoroutine(RequestHelp());
                }
                GUILayout.Label(helpStatus);
                GUILayout.Label("Job Performance:");
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
>>>>>>> Stashed changes
    }
}
