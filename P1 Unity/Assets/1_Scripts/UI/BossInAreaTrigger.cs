using UnityEngine;

public class BossInAreaTrigger : MonoBehaviour
{
    #region references 
    private InputManager player;
    [SerializeField] private HUDController _hudController;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<InputManager>();
        if (player != null)
        {
            _hudController.ShowBossBar(true);
            Destroy(gameObject);
        }

        // Debug.Log(collision.name);
    }
    #endregion
}
