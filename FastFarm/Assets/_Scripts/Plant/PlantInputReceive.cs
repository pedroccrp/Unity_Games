using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantInputReceive : MonoBehaviour {

    public PlantNeedsManager PlantNeeds;

    public ToolsBlueprint toolUsed;    

    private bool isInsideRange = false;
    
    void Update()
    {
        CheckInputs();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isInsideRange = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isInsideRange = false;
        }
    }

    void CheckInputs ()
    {
        if (isInsideRange)
        {
            for (int i = 0; i < GameManager.instance.AllTools.Length; i++)
            {
                if (Input.GetButtonDown(GameManager.instance.AllTools[i].toolName))
                {
                    toolUsed = GameManager.instance.AllTools[i];
                    break;
                }
                else if (i == GameManager.instance.AllTools.Length - 1)
                {
                    toolUsed = null;
                }
            }

            if (toolUsed != null && PlantNeeds.neededTool != null)
            {
                if (PlantNeeds.neededTool == toolUsed)
                {
                    RightActionInputed();
                }
                else
                {
                    Die();
                }
            }
        }
    }

    void RightActionInputed ()
    {
        PlantNeeds.ReceivedRightInput();
    }

    void Die ()
    {
        PlantNeeds.ReceivedWrongInput();
    }
}
