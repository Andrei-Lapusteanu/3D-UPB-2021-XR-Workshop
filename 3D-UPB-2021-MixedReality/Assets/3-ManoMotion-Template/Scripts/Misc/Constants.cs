public static class Constants
{
    public const int LAYER_PHYSICS_OBJECT = 6;
    public const int SCREEN_WIDTH = 2400;
    public const int SCREEN_HEIGHT = 1080;

    public enum GameInitPhase
    {
        PlaneDetection,
        ScenePlacement,
        SceneAdjustments,
        Done
    }

    // This is kind of useless because it only has one element
    // I left it from my old project (and also if you want to add more)
    public enum InteractableType
    {
        PhysicsObject
    }
}
