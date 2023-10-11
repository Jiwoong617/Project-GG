// ���⿡ enum ����
public enum State
{
    Idle,
    Moving,
    Attack,
    Die,
}
/// <summary>
/// ���� Scene�� ���¿� ���� ����
/// </summary>
public enum SceneList
{
    Non, // �������� ��, �� ����
    Init, // �ʱ� Manager���� Scene�� ���ư������� ������ �ʰ� �ϴ� �ʱ� Scene
    AppScene,
    GameScene,
}
public enum BodyParts
{
    Arm,
    Leg,
    Back,
    Chest,
    Shoulder,
}
public enum Gender
{
    Man,
    Woman,
}