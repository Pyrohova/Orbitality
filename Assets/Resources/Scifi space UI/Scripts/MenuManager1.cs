using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuManager1 : MonoBehaviour
{

    public GameObject[] screens;


    private void Update()
    {

    }


    public void OnClick(int index)
    {
        DisableAll();
        screens[index].SetActive(true);
    }

    private void DisableAll()
    {
        foreach (GameObject screen in screens)
            screen.SetActive(false);
    }
    
  
}
