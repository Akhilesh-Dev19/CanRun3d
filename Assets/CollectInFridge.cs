using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectInFridge : MonoBehaviour
{
    public GameObject fridge;

    public GameObject firstpanel;
    public GameObject Secondpanel;
    public GameObject thirdpanel;

    public bool stfilled, ndFilled, rdFilled;
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        gameManager.Roadspeed = 0.1f;

        if (other.CompareTag("blue"))
        {
            if (stfilled != true)
            {
                Destroy(other.gameObject);
                firstpanel.SetActive(true);
                stfilled = true;
            }

            else if (stfilled == true)
            {
                Destroy(other.gameObject);
                Secondpanel.SetActive(true);
                ndFilled = true;
            }
             else if(stfilled == true && ndFilled == true)
            {
                Destroy(other.gameObject);
                thirdpanel.SetActive(true);
                rdFilled = true;
            }

         else if (stfilled == true && ndFilled == true && rdFilled == true)
            {
                fridge.SetActive(false);
            }

        }

       

        gameManager.Roadspeed = 6f;
    }

}
