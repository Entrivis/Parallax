using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    public static InputSystem inputSystem;
    PlayerInputAction playerInput;
    private void Awake() {
        inputSystem = this;
        playerInput = new PlayerInputAction();
        playerInput.Enable();
    }
    public float Movement()
    {
        if (enabled == true)
        {
            return playerInput.PlayerAction.Movement.ReadValue<float>();
        }
        else if (enabled == false)
        {
            return 0;
        }
        return 0;
    }

    public bool Jump(){
        if(enabled == true){
            return playerInput.PlayerAction.Jump.ReadValue<float>() > 0;
        }
        else if(enabled == false){
            return false;
        }
        return false;
    }
    public bool Attack(){
        if(enabled == true){
            return playerInput.PlayerAction.Attack.ReadValue<float>() > 0;
        }
        else if(enabled == false){
            return false;
        }
        return false;
    }
}
