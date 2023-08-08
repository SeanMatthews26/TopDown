using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerControls : MonoBehaviour
{
    private Rigidbody2D rb;
    private Camera mainCam;
    [SerializeField] Grid grid;
    private GridLayout gl;
    private Tilemap tileMap;

    //Movement
    private Vector2 movementInput = Vector2.zero;
    [SerializeField] float playerSpeed;

    //Harvest
    private bool harvesting = false;

    

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        mainCam = Camera.main;
        gl = grid.GetComponent<GridLayout>();
        tileMap = grid.GetComponent<Tilemap>();
    }

    private void OnEnable()
    {

    }

    private void DoHarvest(InputAction.CallbackContext obj)
    {
        harvesting= true;
    }

    private void StopHarvest(InputAction.CallbackContext obj)
    {
        harvesting= false;
    }

    // Update is called once per frame
    void Update()
    {
      
        //FindTile();
    }

    private void FixedUpdate()
    {
        //Player Movement
        Vector2 moveForce = movementInput * playerSpeed * Time.fixedDeltaTime;
        rb.AddForce(moveForce);
    }

    private void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    private void OnUse(InputValue value)
    {
        
    }

    private void FindTile()
    {
        //Find Tile
        Vector2 a = Input.mousePosition;
        Vector2 b = mainCam.ScreenToWorldPoint(a);
        Vector3Int c = gl.WorldToCell(b);
        Debug.Log(c);

        tileMap.GetTile(c);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(!harvesting)
        {
            return;
        }

        Harvestable h = other.gameObject.GetComponent<Harvestable>();

        if(h != null)
        {
            h.Harvest();
        }
    }
}
