using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    [SerializeField] private ViewData viewData;
    [SerializeField] private GameObject viewsContainer;

    // Variabili di stato e riferimenti UI
    [SerializeField] private TextMeshProUGUI titleView;
    [SerializeField] private TextMeshProUGUI previousTitle;
    [SerializeField] private float transitionSpeed = 1.0f;

    private int currentViewIndex = 0;
    private bool isTransitioning = false;
    private int previousViewIndex;
    private string previousViewTitle;

    public enum AppStates { Go, Back, Stay }
    public AppStates state = AppStates.Stay;

    private void Start()
    {
        InitializeViews();
    }

    private void Update()
    {
        switch (state)
        {
            case AppStates.Go:
                NextView();
                break;
            case AppStates.Back:
                PreviousView();
                break;
            case AppStates.Stay:
                Stay();
                break;
        }
    }

    // Inizializza le viste e le aggiunge al contenitore
    private void InitializeViews()
    {
        foreach (var viewInfo in viewData.views)
        {
            GameObject view = Instantiate(viewInfo.viewPrefab, viewsContainer.transform);
            ViewScript viewScript = view.GetComponent<ViewScript>();
            viewScript.titleView = viewInfo.title;
            viewScript.tite.text = viewInfo.title;
            viewScript.description.text = viewInfo.description;
            viewScript.image.texture = viewInfo.image;
            view.SetActive(false);
        }
        viewsContainer.transform.GetChild(currentViewIndex).gameObject.SetActive(true);
    }

    private string GetViewTitle(int index)
    {
        return viewsContainer.transform.GetChild(index).GetComponent<ViewScript>().titleView;
    }

    private void Stay()
    {
        viewsContainer.transform.GetChild(previousViewIndex).gameObject.SetActive(false);
        viewsContainer.transform.GetChild(currentViewIndex).gameObject.SetActive(true);
        titleView.text = GetViewTitle(currentViewIndex);
        previousTitle.text = previousViewTitle;
    }

    public void NextView()
    {
        if (currentViewIndex < viewData.views.Count - 1 && !isTransitioning)
        {
            previousViewIndex = currentViewIndex;
            previousViewTitle = GetViewTitle(currentViewIndex);
            currentViewIndex++;
            StartCoroutine(Transition(transitionSpeed, 1080, 540));
        }
    }

    public void PreviousView()
    {
        if (currentViewIndex > 0 && !isTransitioning)
        {
            previousViewIndex = currentViewIndex;
            currentViewIndex--;
            previousViewTitle = currentViewIndex > 0 ? GetViewTitle(previousViewIndex) : "";
            StartCoroutine(Transition(transitionSpeed, -100, 540));
        }
    }

    public void CloseView()
    {
        previousViewIndex = currentViewIndex;
        currentViewIndex = 0;
        StartCoroutine(Transition(transitionSpeed, -100, 540));
    }

    private IEnumerator Transition(float speed, float startX, float endX)
    {
        float t = 0f;
        isTransitioning = true;

        Transform viewTransform = viewsContainer.transform.GetChild(currentViewIndex);
        Vector2 startPos = new Vector2(startX, viewTransform.position.y);
        Vector2 endPos = new Vector2(endX, viewTransform.position.y);

        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            viewTransform.position = Vector2.Lerp(startPos, endPos, Mathf.Clamp01(t));
            yield return null;
        }

        isTransitioning = false;
    }
}
