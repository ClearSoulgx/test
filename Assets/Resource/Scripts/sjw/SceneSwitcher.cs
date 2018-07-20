using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public  class SceneSwitcher : MonoBehaviour {

    // Use this for initialization
    static float moveTime=0;
	void Start () {
        //StartCoroutine(waitForClick());
        //StartCoroutine(switchScene());
       // DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        //if (Input.touches[0].phase == TouchPhase.Moved && Input.touches[1].phase == TouchPhase.Moved)
        //{
        //    moveTime += Time.deltaTime;
        //    if (moveTime > 1.0f) StartCoroutine(switchScene());
        //}
        //else
        //{
        //    if(moveTime > 0f)  moveTime -= Time.deltaTime;
           
        //}
        //    Debug.Log(moveTime);
    }

    public void SwitchScene(int index)
    {
        Scene cur = SceneManager.GetActiveScene();
        index = ((cur.buildIndex + index) % 3)>=0 ? ((cur.buildIndex + index) % 3):((cur.buildIndex + index) % 3)+3 ;
        //index = ((cur.buildIndex + index) % 3);
        Debug.Log("index : "+index);
        SceneManager.LoadScene(index);
    }

    public void SwitchToScene(int index)
    {
        
        SceneManager.LoadScene(index);
    }

    public void SwitchToScene(string index)
    {

        SceneManager.LoadScene(index);
    }

    IEnumerator switchScene()
    {
        yield return new WaitForSeconds(5f);
        Scene cur = SceneManager.GetActiveScene();
        Debug.Log(cur.name);
        switch (cur.name) {
            case ("pre1_withoutRenderTexture"): SceneManager.LoadScene("pre2");break;
            case ("pre2"): SceneManager.LoadScene("pre3"); break;
            case ("pre3"): SceneManager.LoadScene("pre1_withoutRenderTexture"); break;
            default: SceneManager.LoadScene("pre2"); break;
        }

    }
}
