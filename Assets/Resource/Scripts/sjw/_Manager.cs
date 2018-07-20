using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _Manager : MonoBehaviour
{

    // Use this for initialization
    [SerializeField] private List<GameObject> DontDestroyOnLoadObj ;
    void Start()
    {
        Scene debug = SceneManager.GetActiveScene();
        foreach(GameObject obj in DontDestroyOnLoadObj) DontDestroyOnLoad(obj);
        DontDestroyOnLoad(gameObject);
        Messenger<string>.AddListener(GameEvent.SWITCH_SCENCE, SwitchToScene);
        SwitchToScene(1);
        Debug.Log("switch to scene 1");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchScene(int index)
    {
        Scene cur = SceneManager.GetActiveScene();
        index = ((cur.buildIndex + index) % 3) >= 0 ? ((cur.buildIndex + index) % 3) : ((cur.buildIndex + index) % 3) + 3;
        //index = ((cur.buildIndex + index) % 3);
        Debug.Log("index : " + index);
        SceneManager.LoadScene(index);
    }

    public void SwitchToScene(int index)
    {

        SceneManager.LoadScene(index);
     //   Messenger.Broadcast(GameEvent.SWITCH_SCENCE_FINISHAED);
    }

    public void SwitchToScene(string index)
    {

        SceneManager.LoadScene(index);
    }
}
