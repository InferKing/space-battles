namespace _Project.Scripts.Controller.CameraSystem
{
    using UnityEngine;

    public class AndroidCameraControl : ICameraControl
    {
        private Vector2 _lastTouchPosition;
        private Vector2 _size;
        private bool _isPanning;
        private readonly float _panSpeed = 1.5f;
        private readonly float _zoomSpeed = 0.2f;
        private readonly float _minZoom = 5f;
        private readonly float _maxZoom = 15f;

        public AndroidCameraControl(Vector2 size)
        {
            _size = size;
        }
        
        public void UpdateCamera(Camera camera)
        {
            HandlePan(camera);
            HandleZoom(camera);
        }

        private void HandlePan(Camera camera)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    _isPanning = true;
                    _lastTouchPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved && _isPanning)
                {
                    Vector2 delta = touch.position - _lastTouchPosition;
                    Vector3 move = _panSpeed * Time.deltaTime * new Vector3(-delta.x, -delta.y, 0);

                    Vector3 newPosition = camera.transform.position + move;
                    newPosition.x = Mathf.Clamp(newPosition.x, -_size.x, _size.x);
                    newPosition.y = Mathf.Clamp(newPosition.y, -_size.y, _size.y);
                    
                    camera.transform.position = newPosition;
                    _lastTouchPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    _isPanning = false;
                }
            }
        }

        private void HandleZoom(Camera camera)
        {
            if (Input.touchCount == 2)
            {
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);

                float prevDistance = (touch0.position - touch0.deltaPosition - (touch1.position - touch1.deltaPosition)).magnitude;
                float currentDistance = (touch0.position - touch1.position).magnitude;

                float zoomDelta = (prevDistance - currentDistance) * _zoomSpeed * Time.deltaTime;
                camera.orthographicSize = Mathf.Clamp(camera.orthographicSize + zoomDelta, _minZoom, _maxZoom);
            }
        }
    }

}