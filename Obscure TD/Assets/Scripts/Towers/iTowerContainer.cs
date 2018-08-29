public interface iTowerController {
    bool ContainsTower(Tower tower);
    int TowerCount(Tower tower);
    bool RemoveTower(Tower tower);
    bool AddTower(Tower tower);
    bool IsFull();
}