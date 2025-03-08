using _Project.Scripts.Model.Field;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Controller.CameraSystem
{
    public class CameraController : IInitializable, ITickable
    {
        private readonly FieldParameters _fieldParameters;
        private ICameraControl _cameraControl;
        
        public CameraController(FieldParameters fieldParameters)
        {
            _fieldParameters = fieldParameters;
        }
        
        public void Initialize()
        {
            _cameraControl = new PCCameraControl(_fieldParameters.MapConfig.Size);
        }
        
        public void Tick()
        {
            _cameraControl.UpdateCamera(Camera.main);
        }
    }
}