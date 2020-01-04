using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperlinkScript : MonoBehaviour
{
   private const string url1= "https://www.niddk.nih.gov/health-information/diabetes/overview/what-is-diabetes";
   private const string url2 = "https://www.medicinenet.com/diabetes_treatment/article.htm";
   private const string url3 = "https://www.healthline.com/nutrition/16-best-foods-for-diabetics";
   private const string url4 = "https://www.ncbi.nlm.nih.gov/pmc/articles/PMC3438860/";
   private const string url5 = "https://www.webmd.com/diabetes/taking-care-your-diabetes-every-day#1";

    public void toArticle1()
    {
        Application.OpenURL(url1);
    }
    public void toArticle2()
    {
        Application.OpenURL(url2);
    }
    public void toArticle3()
    {
        Application.OpenURL(url3);
    }
    public void toArticle4()
    {
        Application.OpenURL(url4);
    }
    public void toArticle5()
    {
        Application.OpenURL(url5);
    }
}
