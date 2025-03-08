using UnityEngine;

namespace _Project.Scripts.Controller.CameraSystem
{
    public class PCCameraControl : ICameraControl
    {
        private Vector3 _lastMousePosition;
        private readonly Vector2 _size;
        private readonly float _panSpeed = 5.5f;
        private readonly float _zoomSpeed = 15f;
        private readonly float _minZoom = 5f;
        private readonly float _maxZoom = 35f;
        
        private const string MouseScrollWheel = "Mouse ScrollWheel";

        public PCCameraControl(Vector2 size)
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
            if (Input.GetKeyDown(KeyCode.Mouse0)) 
            {
                _lastMousePosition = Input.mousePosition;
            }

            if (Input.GetKey(KeyCode.Mouse0)) 
            {
                Vector3 delta = Input.mousePosition - _lastMousePosition;
                Vector3 move = _panSpeed * Time.deltaTime * new Vector3(-delta.x, -delta.y, 0);
        
                Vector3 newPosition = camera.transform.position + move;
                newPosition.x = Mathf.Clamp(newPosition.x, -_size.x, _size.x);
                newPosition.y = Mathf.Clamp(newPosition.y, -_size.y, _size.y);

                camera.transform.position = newPosition;

                _lastMousePosition = Input.mousePosition;
            }
        }

        private void HandleZoom(Camera camera)
        {
            float scroll = Input.GetAxis(MouseScrollWheel);
            if (Mathf.Abs(scroll) > 0.01f)
            {
                camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - scroll * _zoomSpeed, _minZoom, _maxZoom);
            }
        }
    }
}