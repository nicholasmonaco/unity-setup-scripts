using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float MaxHealth = 10;
    public float Speed = 5;
    public float SprintMultiplier = 1.5f;

    private Vector2 MoveVector = Vector2.zero;



    void Update() {
        Move();        
    }


    private void Move() {
        // This looks a little weird, so I'll break it down:
        //   - Game.Input is the reference to our InputActions asset (in C# script form)
        //   - .PLayer is the Input set we're reading from - this can be seen/edited in the input asset settings window
        //   - .Move is the action we're reading. WASD is the default for this I beleive
        //   - .ReadValue<Vector2>() means we want to retreive the value of Move as a 2D Vector, one direction for each axis 
        Vector2 rawMoveDirection2d = Game.Input.Player.Move.ReadValue<Vector2>();

        // Since we're caring about a movement direction here, we want any direction combo to be the same amount.
        // In other words, we only care about the direction, not the speed or whatever (at this step).
        // To say that, we normalize the variable. This means the vector will always have a length of 1, which is handy.
        Vector2 normalizedMoveDirection = rawMoveDirection2d.normalized;

        // This is another input example, we're going to check if the sprint button is down this frame.
        // All values are returned as floats, so we have to do the boolean comparison here ourselves.
        bool sprinting = Game.Input.Player.Sprint.ReadValue<float>() > 0;
        
        // Here would be where you apply speed, sprint multipliers, etc
        float speedToApply = Speed;
        if(sprinting == true) 
            speedToApply *= SprintMultiplier;

        MoveVector = normalizedMoveDirection * speedToApply;
    }


    void FixedUpdate() {
        // Since movement is something that means "physics", we do it every FixedUpdate (aka Physics Update) instead of Update (aka Frame Update)
        // Additionally, since transforms are 3D constructs, we have to specify we want to treat our 2D vector that way. This is acheived with a cast.
        transform.position += (Vector3)MoveVector;
    }
}
