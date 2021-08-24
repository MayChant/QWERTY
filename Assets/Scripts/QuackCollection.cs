using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quack Collection", menuName = "Scriptable Objects/Quack Collection", order = 1)]
public class QuackCollection : ScriptableObject
{
    public string[] collection;
}
