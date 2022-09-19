using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabController : MenuController
{
    protected override void Initialization()
    {
        for (int i = 1; i < menus.Length; i++)
        {
            menus[i].CloseMenu();
        }
    }
}
