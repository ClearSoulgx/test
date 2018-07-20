using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitImage : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void reset()
    {
        ArrayList transforms = InitButton.BFS(transform);
        Image image;
        Button button;
        foreach (Transform _transform in transforms)
        {
            image = _transform.GetComponent<Image>();
            if (image != null)
            {
                button = transform.GetComponent<Button>();
                if (button == null)                         //don't be a target of ray if it isn't a button
                {
                    image.raycastTarget = false;
                }
            }
        }
    }
}
