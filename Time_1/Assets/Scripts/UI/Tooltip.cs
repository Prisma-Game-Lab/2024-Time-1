using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;


public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;
    public LayoutElement layoutElement;
    public RectTransform rectTransform;

    public int characterWrapLimit;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void SetText(string content, string header = "")
    {
        if(string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header; 
        }

        contentField.text = content;
        
        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
    }

    public void ShowTooltip()
    {
        gameObject.SetActive(true);
        StartCoroutine(fadeIn(0.3f));
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        /*vector2 position = input.mouseposition;
        float pivotx = position.x / screen.width;
        float pivoty = position.y / screen.height;

        recttransform.pivot = new vector2(pivotx, pivoty);
        transform.position = position.;*/
        var position = Input.mousePosition;

        var normalizedPosition = new Vector2(position.x / Screen.width, position.y / Screen.height);
        var pivot = CalculatePivot(normalizedPosition);
        rectTransform.pivot = pivot;
	    transform.position = position;
    }
    private Vector2 CalculatePivot(Vector2 normalizedPosition)
    {
        var pivotTopLeft = new Vector2(-0.05f, 1.05f);
        var pivotTopRight = new Vector2(1.05f, 1.05f);
        var pivotBottomLeft = new Vector2(-0.05f, -0.05f);
        var pivotBottomRight = new Vector2(1.05f, -0.05f);

        if (normalizedPosition.x < 0.5f && normalizedPosition.y >= 0.5f)
        {
            return pivotTopLeft;
        }
        else if (normalizedPosition.x > 0.5f && normalizedPosition.y >= 0.5f)
        {
            return pivotTopRight;
        }
        else if (normalizedPosition.x <= 0.5f && normalizedPosition.y < 0.5f)
        {
            return pivotBottomLeft;
        }
        else
        {
            return pivotBottomRight;
        }
    }

    IEnumerator fadeIn(float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;

            GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, normalizedTime);
            headerField.color = new Color(headerField.color.r, headerField.color.g, headerField.color.b, normalizedTime);
            contentField.color = new Color(contentField.color.r, contentField.color.g, contentField.color.b, normalizedTime);
            yield return null;
        }

        GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 1);
        headerField.color = new Color(headerField.color.r, headerField.color.g, headerField.color.b, 1);
        contentField.color = new Color(contentField.color.r, contentField.color.g, contentField.color.b, 1);

    }

}

