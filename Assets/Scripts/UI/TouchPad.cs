using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TouchPad : MonoBehaviour
{

    private int touchID = -1;
    private float dragRadius = 80f;
    private bool buttonPressed = false;
    private Vector3 startPos = Vector3.zero;
    private Vector3 diff;
    private RectTransform touchPad;
    private FSMPlayer player;
    private PlayerAction action;
    private Command moveCommand;
    // Start is called before the first frame update
    void Start()
    {
        touchPad = GetComponent<RectTransform>();
        startPos = touchPad.position;
        player = PlayerManager.Instance.Player;
        action = player.GetComponent<PlayerAction>();
        moveCommand = new MoveCommand(action);
        player.SetCommand(-1, moveCommand);
    }

    private void FixedUpdate()
    {
        if (player.isDead() || player.IsEnd()) return;
#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_WEBPLAYER

        HandleInput(Input.mousePosition);

#endif
            HandleTouchInput();
    }
    public void ButtonDowm()
    {
        buttonPressed = true;
    }
    public void ButtonUp()
    {
        buttonPressed = false;
        HandleInput(Vector3.zero);
    }
    void HandleTouchInput()
    {
        int i = 0;
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                i++;
                Vector3 touchPos = Vector3.right * touch.position.x + Vector3.up * touch.position.y;
                if (touch.phase == TouchPhase.Began)
                {
                    if (touch.position.x <= (startPos.x + dragRadius))
                    {
                        touchID = i;
                    }
                }
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    if (touchID == i)
                    {
                        HandleInput(touchPos);
                    }
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    if (touchID == i)
                    {
                        touchID = -1;
                    }
                }
            }
            
        }
    }
    void HandleInput(Vector3 input)
    {
        if (buttonPressed)
        {
            Vector3 diffVector = (input - startPos);
            if (diffVector.sqrMagnitude > dragRadius * dragRadius)
            {
                diffVector.Normalize();
                touchPad.position = startPos + diffVector * dragRadius;
            }

            else
            {
                touchPad.position = input;
            }
        }
        else
        {
            touchPad.position = startPos;

        }
        diff = touchPad.position - startPos;
        Vector2 normDiff = Vector2.right * diff.x / dragRadius+ Vector2.up * diff.y / dragRadius;
        if (player != null)
        {
            player.SetDir(normDiff);
            player.ExecuteCommand(-1);
        }
    }
}
