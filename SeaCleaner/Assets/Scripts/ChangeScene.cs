using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene1 : MonoBehaviour
{
    public bool useAnim = false;

    private LoadingScene loadingScene;

    public string sceneName;

    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("aaa");

        loadingScene = GameObject.FindFirstObjectByType<LoadingScene>();

        float waitDur;

        if (useAnim)
        {
            waitDur = loadingScene.fadeDuration;
        }
    }

    IEnumerator waitForDur(float dur)
    {
        loadingScene.FadeIn();
        yield return new WaitForSeconds(dur);

        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene()
    {
        Time.timeScale = 1f;
        if (useAnim)
        {
            StartCoroutine(waitForDur(1f));
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
        
    }
}
