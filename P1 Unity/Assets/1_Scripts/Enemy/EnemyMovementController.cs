using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    
    #region parameters
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
    private Vector2 dir;             //vector de direccion de enemigo
    private bool derecha;            //si mira hacia derecha el enemigo
    [SerializeField]
    private bool fly=false;
    private Vector2 _fly;
    #endregion

    #region references
    [SerializeField]
    private Transform playerPosition;
    private Rigidbody2D enemy;
    [SerializeField]
    private EnemyDetection _trigger;      //trigger que detectar si hay jugador o no
    [SerializeField]
    private EnemyCollision _col;          //trigger que gestinar cunado el enemigo choca cotra otras cosas 
    private Transform _myTransform;
    [SerializeField]
    private bool haylimite = false;
    [SerializeField]
    private Vector2 limite1;
    [SerializeField]
    private Vector2 limite2;

    #endregion

    #region methods
   
    private void DetectionPlayer()
    {
        detection = true;
        ret = false;
    }
    private void RetrunPlace()
    {
        ret = true;
        detection = false;
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
            if(haylimite&&(enemy.position.x<limite1.x || enemy.position.x > limite2.x))
            {
                RetrunPlace();
            }
        }
        else if (ret)     //volver
        {
            dir = placeOrigin - enemy.position;
            if (Vector2.Distance(enemy.position, placeOrigin) < 1)
            {
                ret = false;
            }
        }
        if (derecha && dir.x < -0.5 || !derecha && dir.x > 0.5)  //giro
        {
            R1();
            Derecha();
        }

        enemy.MovePosition(enemy.position + dir.normalized* _speed * Time.fixedDeltaTime*_fly);
    }
}
