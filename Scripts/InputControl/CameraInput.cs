using InputControl.Template;

namespace InputControl
{
    public class CameraInput
    {
        public InputVerticalModel Zoom { get; set; }
        public InputVerticalModel MouseDeltaVertical { get; set; }
        public InputHorizontalModel MouseDeltaHorizontal { get; set; }
        public InputVerticalModel MousePositionVertical { get; set; }
        public InputHorizontalModel MousePositionHorizontal { get; set; }
        public InputKeyAndReleaseModel MoveOnStartPositionButton { get; set; }
        public InputKeyAndReleaseModel SwitchCameraStateButton { get; set; }
        public InputKeyAndReleaseModel RightMouseButton { get; set; }
    }
}