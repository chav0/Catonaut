using System.Collections;
using System.Collections.Generic;
using ECS;
using UnityEngine;
using Component = ECS.Component;

public class Input : Component
{
    public Vector2 Direction;
    public Vector2 Movement;
    public float Speed;
    public bool Attack; 
    public bool Aim; 
    public bool Aimed; 
}
