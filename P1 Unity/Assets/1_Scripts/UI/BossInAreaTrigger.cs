using UnityEngine;

public class BossInAreaTrigger : MonoBehaviour
{
    #region references 
    private InputManager player;
    [SerializeField] private HUDController _hudController;
    [SerializeField] private GameObject _bossObject;
    #endregion

    #region methods

    public void SpawnBoss()
    {
        _bossObject.SetActive(true);
        _hudController.ShowBossBar(true);

    }

    #endregion
}
