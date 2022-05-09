using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject doorAppear;
    public GameObject doorAppear1;
    public GameObject doorAppear2;
    public float countdown = 3.0f;
    public bool doorDisplay = true;


    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;

     

     if (countdown <= 0.0f)
        {
            if (doorDisplay == true)
            {
                doorDisplay = false;
                doorAppear.SetActive(false);
                doorAppear1.SetActive(false);
                doorAppear2.SetActive(false);

                Debug.Log("Set door inactive");
            }
            else// if(doorDisplay==false)
            {
                Debug.Log("Set door active");

                doorAppear.SetActive(true);
                doorAppear1.SetActive(true);
                doorAppear2.SetActive(true);
                doorDisplay = true;
         
            }
            countdown = 5.0f;
        }
   
    }

 


}
