using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionFailed : MonoBehaviour
{
    public GameObject FailedText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            FailedText.SetActive(true);
        }

        if (other.CompareTag("blue"))
        {
            Destroy(other.gameObject);
            //FailedText.SetActive(true);
        }

    }
}
