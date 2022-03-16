using UnityEngine;

public class BossLifeComponent : MonoBehaviour
{
    #region parameters
    [SerializeField] private int _maxLife = 40;
    [SerializeField] private int _breakPointLife = 20; //donde pasa a la segunda fase
    private bool _secondPhase;
    #endregion

    #region properties
    private int _currentLife;
    #endregion

    #region references
    [SerializeField] private GameObject _DieFX; //pendiente
    [SerializeField] private GameObject _fullBody;

    [SerializeField] private GameObject _weakPoint;
    [SerializeField] private GameObject _bossObject;
    #endregion

    #region methods
    public void Damage(bool isShotUpgraded)
    {
        _currentLife--;
        if (isShotUpgraded) _currentLife--;
        if (_currentLife <= 20 && !_secondPhase)
        {
            _currentLife = 20;

            _weakPoint.GetComponent<Collider2D>().enabled = true;
            _fullBody.GetComponent<Collider2D>().enabled = false;
            _weakPoint.SetActive(true);
            _secondPhase = true;

        } //cambio fase
        if (_currentLife <= 0)
        {
            HUDController.Instance.ShowBossBar(false);
            _bossObject.SetActive(false);
            Instantiate(_DieFX, _bossObject.transform.position, Quaternion.identity);
        }
        HUDController.Instance.UpdateBossHP(_currentLife);
    }

    public int ActualLife() 
    {
        return _currentLife;
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _currentLife = _maxLife;
        _secondPhase = false;

        _weakPoint.SetActive(false);
        _weakPoint.GetComponent<Collider2D>().enabled = false;
        _fullBody.GetComponent<Collider2D>().enabled = true;
        _bossObject.SetActive(false);
    }
}
