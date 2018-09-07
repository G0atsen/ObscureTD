public interface iTowerController {
    //bool ContainsTower(Tower tower);
    int TowerCount(string towerID);
    Tower RemoveTower(string towerID);
    bool RemoveTower(Tower tower);
    bool AddTower(Tower tower);
    bool IsFull();
}