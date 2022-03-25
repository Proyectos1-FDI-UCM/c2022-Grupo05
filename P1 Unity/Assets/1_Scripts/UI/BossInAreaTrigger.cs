using UnityEngine;

public class BossInAreaTrigger : MonoBehaviour
{
    #region references 
    private InputManager player;
    [SerializeField] private HUDController _hudController;
    [SerializeField] private GameObject _bossObject;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<InputManager>();
        if (player != null)
        {
            _bossObject.SetActive(true);
            _hudController.ShowBossBar(true);
        }
    }
    #endregion
}
