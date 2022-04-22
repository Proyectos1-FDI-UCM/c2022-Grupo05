using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _chasespeed = 4;
    [SerializeField]
    private float _speed = 2;                 //velocidad de movimineto 
    [SerializeField]
    private bool detection = false;           //si hay jugador en el campo de vision del enemigo
    [SerializeField]
    private bool ret = false;                 //volver a posicion de origen
    [SerializeField]
    private Vector2 placeOrigin;     //posicion originen
    [SerializeField]
    private int d = 8;               //distancia de movimiento 
    [SerializeField]
    private Vector2 placeObjet;      // posicion final
    [SerializeField]
    private bool fly=false;
    [SerializeField]
    private bool _limite;
    [SerializeField]
    private Vector2 L;
    #endregion

    #region properties
    private float _actualspeed;
    private Vector2 dir;             //vector de direccion de enemigo
    private bool derecha;            //si mira hacia derecha el enemigo
    private Vector2 _fly;
    #endregion
    #region references
    private Transform playerPosition;
    private Rigidbody2D enemy;
    [SerializeField]
    private EnemyDetection _trigger;      //trigger que detectar si hay jugador o no
    [SerializeField]
    private EnemyCollision _col;          //trigger que gestinar cunado el enemigo choca cotra otras cosas 
    private Transform _myTransform;
    #endregion

    #region methods
   
    private void DetectionPlayer()
    {
        _actualspeed = _chasespeed;
        detection = true;
        ret = false;
    }
    private void RetrunPlace()
    {
        ret = true;
        detection = false;
        _actualspeed = _speed;
    }
    private void R1()    //gira 
    {
        _myTransform.Rotate((Vector3.up * 180));

    }
    private void Derecha() 
    {
        derecha = !derecha;
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _actualspeed = _speed;
        playerPosition = PlayerAccess.Instance.Transform;
        enemy = GetComponent<Rigidbody2D>();
        _myTransform = transform;
        placeOrigin = enemy.position;
        placeObjet = enemy.position + new Vector2(d, 0);
        dir = placeObjet - placeOrigin;
        _trigger.accionEntrar += DetectionPlayer;
        _trigger.accionSalir += RetrunPlace;
        _col.accionEntrar += R1;
        _col.accionEntrar += RetrunPlace;
        _col.accionEntrar += Derecha;

        if (dir.x<0)
        {
            derecha = false;
        }
        else
        {
            derecha = true;
        }
        if (fly)
        {
            _fly = Vector2.one;
        }
        else
        {
            _fly = Vector2.right;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!detection && !ret)   //ruta normal
        {
            if (Vector2.Distance(enemy.position, placeObjet) < 1 && placeObjet.x < enemy.position.x)
            {
                dir = placeOrigin - placeObjet;
            }
            else if (Vector2.Distance(enemy.position, placeOrigin) < 1 && placeOrigin.x < enemy.position.x)
            {
                dir = placeObjet - placeOrigin;
            }
        }

        else if (detection)  //detectado el jugador
        {
            dir = new Vector2(playerPosition.position.x,playerPosition.position.y)-enemy.position;
            if (_limite)
            {
                if (L.x > enemy.position.x||L.y<enemy.position.x)
                {
                    //  RetrunPlace();
                    dir = Vector2.zero;
                }
            }
        }
        else if (ret)     //volver
        {
            if (enemy.position.x < placeOrigin.x || enemy.position.x > placeObjet.x)
            {
                dir = placeOrigin - enemy.position;
            }
            else
            {
                if (derecha)
                {
                    dir = placeObjet - enemy.position;
                }
                else
                {
                    dir = placeOrigin - enemy.position;
                }
            }          
            if (Vector2.Distance(enemy.position, placeOrigin) < 1|| Vector2.Distance(enemy.position, placeObjet) < 1)
            {
                ret = false;
            }
        }
        if (derecha && dir.x < -0.5 || !derecha && dir.x > 0.5)  //giro
        {
            R1();
            Derecha();
        }

        enemy.MovePosition(enemy.position + dir.normalized* _actualspeed * Time.fixedDeltaTime*_fly);




    }
}
