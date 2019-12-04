using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
public class containerGraph : MonoBehaviour
{
    private RectTransform graphContainer;
    public GameObject error;
    [SerializeField]
    private Sprite circleSprite;
    public static List<int> valList;
    private void Awake()
    {
        
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
        checkList();
        
    }
    public static void updateTheList(int amount)
    {
        valList.Add(amount);
        for(int i=0;i<valList.Count;i++)
        {
            Debug.Log(valList[i]);
        }
    }
    private void checkList()
    {
        if (valList!=null)
        {
            Debug.Log(valList.Count);
            showGraph(valList);
        }
        else
        {
            error.SetActive(true);
        }
    }
   
    private GameObject createCircle(Vector2 anchoredPosition)
    {
        GameObject gameObj = new GameObject("circle", typeof(Image));
        gameObj.transform.SetParent(graphContainer, false);
        gameObj.GetComponent<Image>().sprite = circleSprite;
        RectTransform recTransform = gameObj.GetComponent<RectTransform>();
        recTransform.anchoredPosition = anchoredPosition;
        recTransform.sizeDelta = new Vector2(11, 11);
        recTransform.anchorMin = new Vector2(0, 0);
        recTransform.anchorMax = new Vector2(0, 0);

        return gameObj;

    } 
    private void showGraph(List<int> valueList)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = 100f;
        float xSize = 50f;
        GameObject lastCircleGameOjbect = null;
        for(int i=0; i< valueList.Count;i++)
        {
            float xPosition = xSize+ i * xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            GameObject circleGameobject = createCircle(new Vector2(xPosition, yPosition));
            if(lastCircleGameOjbect != null)
            {
                CreateConnection(lastCircleGameOjbect.GetComponent<RectTransform>().anchoredPosition, circleGameobject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameOjbect = circleGameobject;
        }
    }
    private void CreateConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gamobj2 = new GameObject("dotConnection", typeof(Image));
        gamobj2.transform.SetParent(graphContainer, false);
        gamobj2.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        RectTransform rectTransform = gamobj2.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA+ dir*distance*.5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));

    }

}
