using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public Image fadeImage; // Reference to the Image component used for the fade effect
    public float fadeDuration = 1.0f; // Duration of the fade effect

    public GameObject LoadCanvas;

    public Image loadBar;
    private bool loadBarDone = false;

    public GameObject LoadBarObj;
    public GameObject LoadBarBGObj;
    public GameObject TitleObj;

    public float loadBarDuration = 5f;
    public float currLoadBar = 0f;

    private bool AlreadyAnim = false;

    public bool useLoadAnim = true;
    void Start()
    {
        // Ensure the image is visible and fully opaque at the start
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);
        fadeImage.gameObject.SetActive(true);

        // Start with fade out effect

        
        
    }

    private void Update()
    {
        if (!AlreadyAnim)
        {
            if (useLoadAnim)
            {
                if (!loadBarDone)
                {
                    LoadingBar();
                }

                if (loadBarDone)
                {
                    StartCoroutine(FadeOut());
                    AlreadyAnim = true;
                }
            }
            else
            {
                LoadCanvas.SetActive(false);
                AlreadyAnim = true;
            }
        }
    }

    public void FadeIn()
    {
        LoadCanvas.SetActive(true);
        // Start the fade in coroutine
        StartCoroutine(FadeInCoroutine());
    }

    void LoadingBar()
    {
        currLoadBar += Time.deltaTime;

        loadBar.fillAmount = currLoadBar / loadBarDuration;

        if (currLoadBar > loadBarDuration) 
        { 
            LoadBarObj.SetActive(false);
            LoadBarBGObj.SetActive(false);
            TitleObj.SetActive(false);
            loadBarDone = true;
        }
    }

    IEnumerator FadeInCoroutine()
    {
        // Ensure the image is visible
        fadeImage.gameObject.SetActive(true);

        LoadCanvas.SetActive(true);

        // Gradually increase alpha over time
        for (float t = 0.0f; t < fadeDuration; t += Time.deltaTime)
        {
            Color color = fadeImage.color;
            color.a = Mathf.Lerp(0, 1, t / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        // Ensure alpha is fully set to 1
        Color finalColor = fadeImage.color;
        finalColor.a = 1;
        fadeImage.color = finalColor;


        // Load the next scene
    }

    IEnumerator FadeOut()
    {
        // Gradually decrease alpha over time
        for (float t = 0.0f; t < fadeDuration; t += Time.deltaTime)
        {
            Color color = fadeImage.color;
            color.a = Mathf.Lerp(1, 0, t / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        // Ensure alpha is fully set to 0
        Color finalColor = fadeImage.color;
        finalColor.a = 0;
        fadeImage.color = finalColor;

        LoadCanvas.SetActive(false);

        // Optionally, disable the image
        fadeImage.gameObject.SetActive(false);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        // If running in the Unity Editor, stop playing
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // If running in a build, quit the application
        Application.Quit();
#endif
    }
}
