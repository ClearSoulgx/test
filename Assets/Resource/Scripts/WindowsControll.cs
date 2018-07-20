using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsControll : MonoBehaviour {
    public GameObject character;
    public GameObject panel;
    public GameObject blackPanel;
    public GameObject[] tips;
    private Animator animator;
    private GameObject currentips;

    private void Awake()
    {
        animator = panel.GetComponent<Animator>();
        panel.gameObject.SetActive(true);
    }
    private void Start()
    {
        currentips = tips[0];
    }
    // Use this for initialization
    public void show()
    {
        animator.SetBool("isshow", false);
        blackPanel.transform.position = new Vector3(0, 0, 0);
        character.SetActive(false);
    }
    public void close()
    {
        animator.SetBool("isshow", true);
        blackPanel.transform.position = new Vector3(0, 0, 9000);
        StartCoroutine(SetTrue());
    }
    IEnumerator SetTrue()
    {
        yield return new WaitForSeconds(0.15f);
        character.SetActive(true);
    }
    public void OpenTips0()
    {
        currentips.transform.localScale = new Vector3(1, 1, 0);
        tips[0].gameObject.transform.localScale = new Vector3(1, 1, 1);
        currentips = tips[0];
    }
    public void OpenTips1()
    {
        currentips.transform.localScale = new Vector3(1, 1, 0);
        tips[1].gameObject.transform.localScale = new Vector3(1, 1, 1);
        currentips = tips[1];
    }
    public void OpenTips2()
    {
        currentips.transform.localScale = new Vector3(1, 1, 0);
        tips[2].gameObject.transform.localScale = new Vector3(1, 1, 1);
        currentips = tips[2];
    }
    public void OpenTips3()
    {
        currentips.transform.localScale = new Vector3(1, 1, 0);
        tips[3].gameObject.transform.localScale = new Vector3(1, 1, 1);
        currentips = tips[3];
    }
    public void CloseTips()
    {
        currentips.transform.localScale = new Vector3(1, 1, 0);
    }
}
