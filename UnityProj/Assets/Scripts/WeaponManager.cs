using UnityEngine;
using System.Collections;

/// <summary>
///     Weapon controller : manage weapon inventory + fight inputs.
/// </summary>
public class WeaponManager : MonoBehaviour
{
    /// <summary>
    ///     Current used weapon.
    /// </summary>
    public WeaponBase weapon;

    private void Update()
    {
        if (Input.GetButtonUp("Fire1") == true)
        {
            this.weapon.Attack(false);
        }
        else if (Input.GetButtonUp("Fire2") == true)
            this.weapon.Attack(true);
    }
}
