// 여기에 enum 정의
public enum State
{
    None,
    Idle,
    Moving,
    Attack,
    Die,
}
/// <summary>
/// 현재 Scene의 상태에 대한 정의
/// </summary>
public enum SceneList
{
    None, // 미지정일 때, 즉 오류
    Init, // 초기 Manager들이 Scene에 돌아갈때마다 생기지 않게 하는 초기 Scene
    AppScene,
    GameScene,
}
public enum BodyParts
{
    Arm,
    Leg,
    Back,
    Chest,
    Abs
}
public enum Gender
{
    Man,
    Woman,
}