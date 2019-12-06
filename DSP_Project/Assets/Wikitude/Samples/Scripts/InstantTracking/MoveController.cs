using UnityEngine;

/* Script that controls dragging the furniture augmentations on the screen. */
public class MoveController : MonoBehaviour
{
    private Transform _activeObject = null;

    private Vector3 _startObjectPosition;
    private Vector2 _startTouchPosition;
    private Vector2 _touchOffset;

    private InstantTrackingController _controller;
    public AudioSource[] myAudioSource;
    private void Start() {
        _controller = GetComponent<InstantTrackingController>();
    }

    public Transform ActiveObject {
        get {
            return _activeObject;
        }
    }

    public void SetMoveObject(Transform newMoveObject) {
        if (_controller.ActiveModels.Contains(newMoveObject.gameObject)) {
            _activeObject = newMoveObject;
            _startObjectPosition = _activeObject.position;
            _startTouchPosition = Input.GetTouch(0).position;
            _touchOffset = Camera.main.WorldToScreenPoint(_startObjectPosition);
        }
    }

    void Update () {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            RaycastHit hit;

            if (Input.touches[0].phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                if (Physics.Raycast(ray, out hit))
                {
                    
                    switch (hit.transform.name)
                    {
                        case "Sphere":
                            Destroy(hit.transform.gameObject);
                            myAudioSource[0].Play();
                            break;
                        case "leftBody":
                            buttonManager.leftBody = true;
                            myAudioSource[2].Play();
                            break;
                        case "rightBody":
                            buttonManager.rightBody = true;
                            myAudioSource[1].Play();
                            break;
                        case "leftHair":
                            buttonManager.leftHair = true;
                            myAudioSource[2].Play();
                            break;
                        case "rightHair":
                            buttonManager.rightHair = true;
                            myAudioSource[1].Play();
                            break;
                        case "leftKit":
                            buttonManager.leftKit = true;
                            myAudioSource[2].Play();
                            break;
                        case "rightKit":
                            buttonManager.rightKit = true;
                            myAudioSource[1].Play();
                            break;
                        case "revert":
                            buttonManager.revert = true;
                            myAudioSource[1].Play();
                            break;
                        case "toMenu":
                            buttonManager.save = true;
                            myAudioSource[1].Play();
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {

            
            /* If we're currently not dragging any augmentation, do a raycast to find one in the scene. */
            if (_activeObject == null) {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out hit)) {
                    SetMoveObject(hit.transform);
                }
            }

            /* If we are dragging an augmentation, raycast against the ground plane to find how the augmentation should be moved. */
            if (_activeObject != null) {
                var screenPosForRay = (touch.position - _startTouchPosition) + _touchOffset;
                Ray cameraRay = Camera.main.ScreenPointToRay(screenPosForRay);
                Plane p = new Plane(Vector3.up, Vector3.zero);

                float enter;
                if (p.Raycast(cameraRay, out enter)) {
                    var position = cameraRay.GetPoint(enter);

                    /* Clamp the new position within reasonable bounds and make sure the augmentation is firmly placed on the ground plane. */
                    position.x = Mathf.Clamp(position.x, -15.0f, 15.0f);
                    position.y = 0.0f;
                    position.z = Mathf.Clamp(position.z, -15.0f, 15.0f);

                    /* Lerp the position of the dragged augmentation so that the movement appears smoother */
                    _activeObject.position = Vector3.Lerp(_activeObject.position, position, 0.25f);
                }
            }

        }
        } else {
            /* If there are no touches, stop dragging the currently dragged augmentation, if there is one. */
            _activeObject = null;
        }
    }

}
