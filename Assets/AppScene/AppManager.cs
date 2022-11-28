using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AppManager : MonoBehaviour
{

    [SerializeField] GameObject views;


    //PER DEBUG USO SERIALIZEFIELD
    [SerializeField] Text TitleView;
    int actualView;
    [SerializeField] float speedTransiction;
    bool isTransiction;
    int tempActualView;
    public AppStates states;

    public enum AppStates { 
        go,
        back,
        stay
    }
    
    // Start is called before the first frame update
    void Start()
    {
        states = AppStates.stay;
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (states) {
            case AppStates.go:
                _GO();
                break;
            case AppStates.back:
                _BACK();
                break;
            case AppStates.stay:
                _STAY();
                break;
        }
    }


    public void _GO() { 
    
    }

    public void _BACK()
    {

    }

    public void _STAY()
    {
        views.transform.GetChild(tempActualView).gameObject.SetActive(false);  //tempActive
        views.transform.GetChild(actualView).gameObject.SetActive(true); //actualActive
        TitleView.text= views.transform.GetChild(actualView).gameObject.GetComponent<ViewScript>().title;
   
    }


    public void _buttonAvanti() {
        if ((actualView < views.transform.childCount-1) && !isTransiction)
        {
            tempActualView = actualView;
            actualView += 1;
            StartCoroutine(Transiction(speedTransiction));
        }
    }

    public void _buttonIndietro() {
        if (actualView > 0 && !isTransiction)
        {
            tempActualView = actualView;
            actualView -= 1;
            StartCoroutine(Transiction(speedTransiction));
        }
    }

    public void _buttonChiudi() {

        tempActualView = actualView;
        actualView = 0;
        StartCoroutine(Transiction(speedTransiction));
    }


    private IEnumerator Transiction(float speed)
    {
        float t = 0f;
        isTransiction= true;
        Vector2 startpos = new Vector2(-100, views.transform.GetChild(0).position.y);
        Vector2 endpos = new Vector2(540, views.transform.GetChild(0).position.y);
        while (t < 0.5f)
        {
            t += Time.deltaTime; 
            if (t > 1) t = 1;
            views.transform.GetChild(actualView).position=Vector2.Lerp(startpos, endpos, t*speed);
            yield return null;
        }
        isTransiction = false;
    }

}
