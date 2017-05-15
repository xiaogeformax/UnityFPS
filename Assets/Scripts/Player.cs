using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    
    private GameObject currentGo;

    public GameObject targetPrefab;
    public GameObject Target;


    //组件
    public Transform m_transform;
    CharacterController m_ch;

    //角色移动速度
    float m_movSpeed = 3.0f;

    //重力
    float m_gravity = 2.0f;

    //生命值
    public int m_life = 5;

    //摄像机Transform
    Transform m_camTransform;

    //摄像机旋转角度
    Vector3 m_camRot;

    //摄像机高度
    float m_camHeight = 0.6f;

    //枪口transform
    Transform m_muzzlepoint;

    //射击时，射线射到的碰撞层
    public LayerMask m_layer;

    //射中目标后的粒子效果
    public Transform m_fx;

    //特效
    public GameObject m_texiao;
    //射击音效
    public AudioClip m_audio;

    //背景音乐
    public AudioClip m_musicClip;

    AudioSource soundSource;

    //射击间隔时间计时器
    float m_shootTimer = 0;

    void Awake()
    {

        
        //获取组件
        m_transform = this.transform;

    }

    // Use this for initialization
    void Start()
    {
        m_ch = this.GetComponent<CharacterController>();

        soundSource = this.GetComponent<AudioSource>();

        //获取摄像机
        m_camTransform = Camera.main.transform;

        //设置摄像机初始位置
        Vector3 pos = m_transform.position;
        pos.y += m_camHeight;
        m_camTransform.position = pos;

        m_camTransform.rotation = m_transform.rotation;
        m_camRot = m_camTransform.eulerAngles;

        //锁定鼠标
        //Screen.lockCursor = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        m_muzzlepoint = m_camTransform.FindChild("M16/weapon/muzzlepoint").transform;


    }


    void OnGUI()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));//射线  

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))//发射射线(射线，射线碰撞信息，射线长度，射线会检测的层级)  
        {
            Vector3 position = new Vector3(hit.point.x, 0, hit.point.z);
        }

    }



    // Update is called once per frame
    void Update()
    {
        if (m_life <= 0)
        {
            return;
        }

        Control();

        //更新射击间隔时间  每隔0.1秒射击一次
        m_shootTimer -= Time.deltaTime;
        if (Input.GetMouseButton(0) && m_shootTimer <= 0)
        {
            m_shootTimer = 0.1f;

            soundSource.PlayOneShot(m_audio, 1);

            //减少弹药，更新弹药UI
            GameManager.Instance.SetAmmo(1);

            //RaycastHit用来保存射线的探测结果
            RaycastHit info;


            bool hit = Physics.Raycast(m_muzzlepoint.position, m_camTransform.TransformDirection(Vector3.forward), out info, 100, m_layer);
            //从muzzlepoint的位置，想摄像机面向的正方向射出一根射线
            //射线只能与m_layer所指定的层碰撞
            //bool hit = Physics.Raycast( m_muzzlepoint.position, m_camTransform.TransformDirection( Vector3.forward ), out info, 100, m_layer );
            if (hit)
            {
                Debug.Log("hit is true");
                if (info.transform.tag.CompareTo("enemy") == 0)
                {
                    Debug.Log("hit enemy is true");
                    Enemy enemy = info.transform.GetComponent<Enemy>();

                    //敌人减少声明
                    enemy.OnDamage(1);
                }
                //实例化子弹
               Instantiate( m_fx, info.point, info.transform.rotation );
               /* Destroy(currentGo);
                Destroy(Target);
                currentGo = Instantiate(m_texiao, transform.position, m_texiao.transform.rotation) as GameObject;

                Target = Instantiate(targetPrefab, info.point, info.transform.rotation) as GameObject;
                effectSettings.MoveSpeed = 50f;
                effectSettings = currentGo.GetComponent<EffectSettings>();
                
               effectSettings.Target = Target;*/


            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void Control()
    {
        //获取鼠标移动距离
        float rh = Input.GetAxis("Mouse X");
        float rv = Input.GetAxis("Mouse Y");

        //旋转摄像机
        m_camRot.x -= rv;
        m_camRot.y += rh;
        m_camTransform.eulerAngles = m_camRot;

        //使主角的面向方向与摄像机一致
        Vector3 camrot = m_camTransform.eulerAngles;
        camrot.x = 0;
        camrot.z = 0;
        m_transform.eulerAngles = camrot;


        //操作主角移送
        float xm = 0, ym = 0, zm = 0;

        //重力运动
        ym -= m_gravity * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
        {
            zm += m_movSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            zm -= m_movSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            xm -= m_movSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            xm += m_movSpeed * Time.deltaTime;
        }

        //移动
        m_ch.Move(m_transform.TransformDirection(new Vector3(xm, ym, zm)));

        //使摄像机的位置与主角一致
        Vector3 pos = m_transform.position;
        pos.y += m_camHeight;
        m_camTransform.position = pos;

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(this.transform.position, "Spawn.tif");
    }

    public void OnDamage(int damage)
    {
        m_life -= damage;

        //更新UI
        GameManager.Instance.SetLife(m_life);

        BloodScreen.Instance.Show();
        //如果生命为0，取消鼠标锁定
        if (m_life <= 0)
        {
            //Screen.lockCursor = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
