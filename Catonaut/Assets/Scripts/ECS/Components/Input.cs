using System.Collections;
using System.Collections.Generic;
using ECS;
using UnityEngine;
using Component = ECS.Component;

public class Input : Component
{
    public float Direction;
    public Vector2 Movement;
    public float Speed; 
}
