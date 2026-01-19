using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Manager<WeaponManager>
{
    public List<WeaponData> allWeapons;
    public Loadout loadout = new Loadout();
}