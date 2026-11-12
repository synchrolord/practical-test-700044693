using System;
using UnityEngine;


    [CreateAssetMenu(
    fileName = "MoonSettings",
    menuName = "Moon Hop/Moon Settings")]
    

public class MoonSettinggs : ScriptableObject
{
[Header ("Movement")]
public float moveSpeed =12f;
public float jumpStrength =4f;
public float deceleration = 25f;

public float gravity=1.625f;
public float GroundCheck = 1.02f;



}
