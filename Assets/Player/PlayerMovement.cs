using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    



    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;
    [SerializeField] float clickToMoveRadius = .02f;
    bool isInDirectMode = false; //TODO consider making static later

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics

    // TODO fix issue with click to move and WASD conflicting and increasingspeed

    private void FixedUpdate()
    {
        //G for gamepad. TODO allow players to map later;
        if (Input.GetKeyDown(KeyCode.G))
        {
            isInDirectMode = !isInDirectMode;
        }

        if (isInDirectMode)
        {
            ProcessDirectMovement();
        }
        else
        {
            ProcessMouseMovement();
        }
        
    }

    private void ProcessDirectMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        print(h + v);


        //Calculate camera relative direction to move
        Vector3 m_camForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 m_Move = v * m_camForward + h * Camera.main.transform.right;

        m_Character.Move(m_Move, false, false);

    }


    private void ProcessMouseMovement()
    {
        if (Input.GetMouseButton(0))
        {
            print("Cursor raycast hit layer: " + cameraRaycaster.layerHit);

            switch (cameraRaycaster.layerHit)
            {
                case Layer.Walkable:
                    currentClickTarget = cameraRaycaster.hit.point;
                    break;

                case Layer.Enemy:
                    print("Not moving towards Enemy");
                    break;

                default:
                    print("Unexpected LayerBound");
                    break;
            }

        }

        var clickToMove = currentClickTarget - transform.position;
        if (clickToMove.magnitude >= clickToMoveRadius)
            m_Character.Move(currentClickTarget - transform.position, false, false);
        else
        {
            m_Character.Move(Vector3.zero, false, false);
        }
    }
}

