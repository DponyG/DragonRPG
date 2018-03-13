using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;
    [SerializeField] float clickToMoveRadius = .02f;
        
    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
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
        if(clickToMove.magnitude >= clickToMoveRadius) 
        m_Character.Move(currentClickTarget - transform.position, false, false);
        else 
        {
            m_Character.Move(Vector3.zero, false, false);
        }

    }
}

