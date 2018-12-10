using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotMouseInteractionsHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    private GameObject Canvas; //Entire screen canvas
    private Transform OldParent;
    private Vector2 mouseOffset;

    private Image SlotImage;
    public float grabbedIngredientSizeMultiplier = 2f;

    private bool isDragging = false;
    private bool willDropIngredient = false;

    public LayerMask DropLayer;

    public Image BorderImage;
    public Color normalColor;
    public Color hoveringColor;
    [Range(0, 0.1f)]
    public float colorChangeSpeed;

    private void Awake()
    {
        Canvas = GameObject.Find("Canvas");
        SlotImage = transform.Find("SlotImage").gameObject.GetComponent<Image>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && !isDragging && !PotionSceneManager.instance.isGameOver && !PotionSceneManager.instance.isPotionFinished)
        {
            SendInfoForDescriptionWindow();
        }
    }

    private void OnMouseEnter()
    {
        GetComponent<IngredientSlot>().slotAnim.SetBool("isHovering", true);
        SelectSlot();
    }

    private void OnMouseExit()
    {
        GetComponent<IngredientSlot>().slotAnim.SetBool("isHovering", false);
        DeselectSlot(true);
    }

    //Sends information about Ingredient to open the description window
    private void SendInfoForDescriptionWindow()
    {
        UILoading.instance.OpenDescriptionWindow(GetComponent<IngredientSlot>().ingredientOnSlot);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0) && !isDragging && !PotionSceneManager.instance.isDropping)
        {
            OldParent = transform.parent;
            transform.SetParent(Canvas.transform);
            mouseOffset = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
            SlotImage.enabled = false;
            BorderImage.enabled = false;

            gameObject.transform.localScale *= grabbedIngredientSizeMultiplier;

            DeselectSlot(false);

            isDragging = true;

            UILoading.instance.CloseDescriptionWindow();
        }        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0) && isDragging)
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseOffset;        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            Vector3 DropPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Ray ray = new Ray(DropPoint, Vector3.forward * 20);

            foreach (RaycastHit h in Physics.RaycastAll(ray))
            {
                if (h.transform.gameObject.layer == LayerMask.NameToLayer("Cauldron"))
                {
                    willDropIngredient = true;
                    break;
                }
            }

            if (willDropIngredient)// When the object is dropped at the potion
            {
                InsertIngredientOnCauldron();
                
                Destroy(gameObject);
                willDropIngredient = false;                
            }
            else
            {
                SlotImage.enabled = true;
                BorderImage.enabled = true;
                transform.SetParent(OldParent);
                transform.SetAsLastSibling();
                gameObject.transform.localScale /= grabbedIngredientSizeMultiplier;
            }

            isDragging = false;
        }        
    }

    private void SelectSlot ()
    {
        StopAllCoroutines();

        StartCoroutine(SmoothColorChange(BorderImage, BorderImage.color, hoveringColor, colorChangeSpeed));
    }

    private void DeselectSlot (bool changeSmoothly)
    {
        StopAllCoroutines();

        if (changeSmoothly)
        {        
            StartCoroutine(SmoothColorChange(BorderImage, BorderImage.color, normalColor, colorChangeSpeed));
        }
        else
        {
            BorderImage.color = normalColor;
        }
    }

    private IEnumerator SmoothColorChange (Image I, Color c1, Color c2, float smoothSpeed)
    {
        float t = 0;

        while (t < 1)
        {
            I.color = Color.Lerp(c1, c2, t);            

            t += smoothSpeed;

            yield return null;
        }
    }

    private void InsertIngredientOnCauldron()
    {
       DropIngredient.instance.Drop(GetComponent<IngredientSlot>().ingredientOnSlot);

        //Close Riddle Box
        PotionSceneManager.instance.RiddleCanvas.SetActive(false);   
    
    }
}
