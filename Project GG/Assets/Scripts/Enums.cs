// ���⿡ enum ����
public enum State
{
    Idle,
    Moving,
    Attack,
    Dodge,
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