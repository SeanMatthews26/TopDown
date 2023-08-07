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

    //Input
    public PlayerInput playerActionAsset;
    private InputAction moveInput;

    //Movement
    private Vector2 movement;

    //Harvest
    private bool harvesting = false;

    [SerializeField] float playerSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        playerActionAsset = new PlayerInput();

        rb = GetComponent<Rigidbody2D>();

        mainCam = Camera.main;
        gl = grid.GetComponent<GridLayout>();
        tileMap = grid.GetComponent<Tilemap>();
    }

    private void OnEnable()
    {
        playerActionAsset.Enable();
        moveInput = playerActionAsset.Player.Move;

        playerActionAsset.Player.Harvest.started += DoHarvest;
        playerActionAsset.Player.Harvest.canceled += StopHarvest;
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
        movement = Vector2.zero + moveInput.ReadValue<Vector2>();

        rb.velocity = movement * Time.deltaTime * playerSpeed;

        //FindTile();
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
