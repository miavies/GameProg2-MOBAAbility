using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    public enum TargetableType { Enemy, Ally }
    public TargetableType TargetType;
}
