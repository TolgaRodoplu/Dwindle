using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalButton : MonoBehaviour
{
    public ButtonType btnType;
    // Start is called before the first frame update
    public void Pressed()
    {
        EventSystem.instance.ButtonAction(btnType);
    }

}

public enum ButtonType 
{
    Play,
    Settings,
    Exit
};
