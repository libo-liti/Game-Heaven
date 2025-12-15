public class War_DestroyLaser : War_BossLaser
{
    void Update()
    {
        RangeOutOfField();      // 범위 이탈시 파괴
    }
}
