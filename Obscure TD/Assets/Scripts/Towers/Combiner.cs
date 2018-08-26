using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct NumberOfTowers{
    public Tower Tower;
    [Range(1,5)]
    public int number;
}

[CreateAssetMenu]
public class Combiner : ScriptableObject {
    public List<NumberOfTowers> components;
    public List<NumberOfTowers> output;


}
