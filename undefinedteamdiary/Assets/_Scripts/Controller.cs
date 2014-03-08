using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Controller : MonoBehaviour 
{
    public float MoveSpeed;
    public float RoationSpeed;
    CharacterController cc;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        cc.SimpleMove(Physics.gravity);
    }
}
