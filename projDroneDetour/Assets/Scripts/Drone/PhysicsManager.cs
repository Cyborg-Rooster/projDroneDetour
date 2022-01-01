using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
class PhysicsManager
{
    public Rigidbody2D Rigidbody2D;
    public float JumpForce;
    public bool State;

    public void Jump()
    {
        Rigidbody2D.velocity = Vector2.zero;
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    public void SetComponentState(bool _state)
    {
        State = _state;
        Rigidbody2D.bodyType = _state ? RigidbodyType2D.Dynamic : RigidbodyType2D.Static;
    }
}
