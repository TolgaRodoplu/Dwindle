using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    bool soundPlayed = false;

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            
            var btn = hit.transform.GetComponent<PhysicalButton>();

            if (btn != null)
            {
                if (!soundPlayed)
                {
                    soundPlayed = true;
                    FindObjectOfType<AudioManeger>().Play("Hover");
                }

                if (Input.GetMouseButtonDown(0))
                {
                    FindObjectOfType<AudioManeger>().Play("Click");
                    EventSystem.instance.ButtonAction(btn.btnType);
                }
            }
            else
                soundPlayed = false;

        }
        
    }
}
