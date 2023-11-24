using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : SerializedSingleTion<InputManager>
{
    public GamePlay gamePlay;
    public Vector3 mousePostion;//鼠标位置
    public bool isOnGUI;//是否在ui
    protected override void Awake()
    {
        base.Awake();
        if (gamePlay == null)
            gamePlay = new GamePlay();
        UseGamePlay();
    }
    public void UseGamePlay()
    {
        gamePlay.Play.MouseMove.performed += MouseMove;
        gamePlay.Play.Rotate.performed += Rotate;
        gamePlay.Play.LeftClick.performed += LeftClick;

        gamePlay.Play.Enable();
    }

    private void LeftClick(InputAction.CallbackContext obj)
    {
        GamePlayManager.Instance.LeftClick();
    }

    private void Rotate(InputAction.CallbackContext obj)
    {
        GamePlayManager.Instance.Rotate();
    }

    private void MouseMove(InputAction.CallbackContext obj)
    {
        mousePostion = obj.ReadValue<Vector2>();
        GamePlayManager.Instance.UpdateMousePos(mousePostion);
    }

}
