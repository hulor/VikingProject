using UnityEngine;
using System.Collections;

/// <summary>
///     Controller for weapons and attacks.
/// </summary>
public class FightController : MonoBehaviour
{
    public WeaponBase weapon;

    private void Update()
    {
        if (Input.GetButtonUp("Jump") == true)
            weapon.Attack();
    }
}
