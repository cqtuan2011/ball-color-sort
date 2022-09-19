using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] protected MenuItem[] menus;

    private void Start()
    {
        Initialization();
    }

    protected virtual void Initialization()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].CloseMenu();
        }
    }

    public void OpenMenu(string menuName)
    {
        foreach( var menu in menus)
        {
            if (menu.menuName == menuName)
            {
                menu.OpenMenu();
            } else
            {
                menu.CloseMenu();
            }
        }
    }

    public void CloseMenu(string menuName)
    {

        foreach (var menu in menus)
        {
            if (menu.menuName == menuName)
            {
                menu.CloseMenu();
            }
        }
    }
}
