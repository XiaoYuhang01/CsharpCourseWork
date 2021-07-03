using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enter1 : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            SceneManager.LoadScene(2);
        }
    }
}
