using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mygame;
public enum PlayerState
{
    walk,
    dash,
    attack,
    skillCast
}
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;            // The speed that the player will move at.
    public float attackCoolDown = 0.2f;//攻击间隔
    public PlayerState state;
    float attackTimeStamp;//攻击时间戳
    float dashTimeStamp;//冲刺时间戳
    public float dashSpeed = 15f;//冲刺速度
    public float dashLast = 0.4f;//冲刺持续时间
    public PlayerManager playerManager;
    Vector3 dashVector;//决定冲刺方向的向量
    CharacterController moveController;
    public 
    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.

    public static Vector3 VectorRotate(Vector3 input, float angle)
    {
        float cos = Mathf.Cos(angle* Mathf.Deg2Rad);
        float sin = Mathf.Sin(angle* Mathf.Deg2Rad);
        Matrix4x4 m = new Matrix4x4(new Vector4(cos, 0, -sin, 0), new Vector4(0, 1, 0, 0), new Vector4(sin, 0, cos, 0), new Vector4(0, 0, 0, 1));
        Vector3 output = m.MultiplyPoint(input);
        return output;
    }

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        attackTimeStamp = Time.time;
        state = PlayerState.walk;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        moveController = GetComponent<CharacterController>();
        playerManager = GetComponent<PlayerManager>();
        ItemManager.instance.equip(new ItemBlock (ItemManager.instance.cover.itemList[2]));
    }
    void FixedUpdate()
    {
        attackCoolDown = ItemManager.instance.nowWeapon.item.atkCooldown;
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Dash(h,v);
        Move(h, v);//移动方法
        Turning();//todo: 转向方法，未完成
        Attack();//攻击方法
        Cast();
    }
    
    void fire(Vector3 playerToMouse, Item nowWeapon,float angle)
    {
        GameObject bullet;
        bullet = Instantiate(Resources.Load("Prefab/TestBullet"), transform.position, new Quaternion()) as GameObject;
        bullet.GetComponent<TestBullet>().damage = new Damage(nowWeapon.maxatk, nowWeapon.minatk, nowWeapon.critRate, nowWeapon.critPower,playerManager);
        bullet.GetComponent<TestBullet>().fire(VectorRotate(playerToMouse, angle), nowWeapon.bulletSpeed);
    }
    void Attack()//攻击
    {
        if (Input.GetButton("Fire1") && Time.time >= attackTimeStamp && ItemManager.instance.nowWeapon != null)
        {
            Item nowWeapon = ItemManager.instance.nowWeapon.item;//获取当前武器
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;
            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
            {
                Vector3 playerToMouse = floorHit.point - transform.position;
                playerToMouse.y = 0f;
                if (nowWeapon.type == ItemType.pistol||nowWeapon.type == ItemType.assaultRifle|| nowWeapon.type == ItemType.sniperRifle)
                {
                    playerManager.GetCommand(new AttackCommand());
                    fire(playerToMouse, nowWeapon, 0);
                    for (int i = 0; i < nowWeapon.fireMultiply - 1; i++)
                    {
                        fire(playerToMouse, nowWeapon, Random.Range(-3*(i+1), 3*(i+1)));
                    }
                    attackTimeStamp = Time.time + attackCoolDown;
                }
                else if (nowWeapon.type == ItemType.shotgun)
                {
                    playerManager.GetCommand(new AttackCommand());
                    for(int i = 0; i < nowWeapon.bulletNum; i++)
                    {
                        fire(playerToMouse, nowWeapon, Random.Range(-30,30));
                    }
                    attackTimeStamp = Time.time + attackCoolDown;
                }
            }
        }
    }
    void Dash(float h,float v)//冲刺
    {
        if (state == PlayerState.dash)
        {
            if (Time.time >= dashTimeStamp - 0.1f&&Time.time<dashTimeStamp)
            {
                dashVector = dashVector * ((dashTimeStamp - Time.time) / 0.1f);
                moveController.SimpleMove(dashVector);
            }
            else
            {
                moveController.SimpleMove(dashVector);
            }
            if (Time.time >= dashTimeStamp)
            {
                state = PlayerState.walk;
            }
        }
        if (Input.GetButtonDown("Jump")&&state!=PlayerState.dash&&(h!=0||v!=0))
        {
            playerManager.GetCommand(new DashCommand(h, v));
            dashVector = new Vector3(h, 0f, v).normalized * dashSpeed;
            state = PlayerState.dash;
            dashTimeStamp = Time.time + dashLast;
        }
    }
    void Cast()//释放技能
    {
        Vector3 aim = Input.mousePosition;
    }
    void Move(float h, float v)
    {

        if (state != PlayerState.dash)
        {
            playerManager.GetCommand(new MoveCommand(h, v));
            // Set the movement vector based on the axis input.
            movement.Set(h, 0f, v);

            // Normalise the movement vector and make it proportional to the speed per second.
            movement = movement.normalized * speed;

            // Move the player to it's current position plus the movement.
            moveController.SimpleMove(movement);
        }
    }

    void Turning()
    {
        
        /*
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation(newRotation);
        }
        */
    }

    void Animating(float h, float v)
    {

    }
}
