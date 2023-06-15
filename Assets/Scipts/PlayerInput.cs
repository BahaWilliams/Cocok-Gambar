using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private void Update()
    {
        InputRaycast();
    }

    private void InputRaycast()
    {
        if (Input.GetMouseButtonDown(0) && !GameManager.Instance.HasPickedCard() && !GameManager.Instance.IsGameOver())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log(hit.transform.gameObject);
                Card currentCar = hit.transform.GetComponent<Card>();

                if(currentCar != null)
                {
                    currentCar.FlippedOpen(true);
                    GameManager.Instance.AddCardFromList(currentCar);
                }
            }
        }
    }
}
