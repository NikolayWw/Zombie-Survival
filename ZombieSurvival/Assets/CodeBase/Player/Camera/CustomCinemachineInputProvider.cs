using Cinemachine;
using CodeBase.Services.Input;

namespace CodeBase.Player.Camera
{
    public class CustomCinemachineInputProvider : CinemachineInputProvider
    {
        private IInputService _inputService;

        public void Construct(IInputService inputService)
        {
            _inputService = inputService;

            _inputService.OnTouchScreenCameraDown += Unfreeze;
            _inputService.OnTouchScreenCameraUp += Freeze;
        }

        private void OnDestroy()
        {
            _inputService.OnTouchScreenCameraDown -= Unfreeze;
            _inputService.OnTouchScreenCameraUp -= Freeze;
        }

        private void Unfreeze() =>
            XYAxis.action.Enable();

        private void Freeze() =>
            XYAxis.action.Disable();
    }
}