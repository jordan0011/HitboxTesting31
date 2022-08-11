using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public CinemachineFreeLook cam1;
    private float camx;
    private float camy;
    private bool ShowCursor = false;

    Vector3 moveDirection1, straight;
    float rotation, _speed = 5f;
    bool isImmobilized = false, _isGrounded = true;

    public GameObject freeCam;
    private float CamRotation;

    private bool _forward;
    private bool _backward;
    private bool _left;
    private bool _right;

    public AbilityLib abilities;
    public PowerStats mypowerstats;
    public LayerMask ignorePlayer;
    public float targetAngle;
    public Vector3 aimdirection;

    public SummoningObject book = null;

    public int allowedBooks = 4;

    public SummoningObject[] books = null;

    public SummoningObject2 book2 = null;

    public SummoningObject2a book2a = null;

    public SummoningObject3 book3 = null;

    public Text doubleangle;
    public int doubleangle1 = 0;

    public GameObject abilitySetsPanel;

    public int gold = 0;
    void Start()
    {
        camx = 140;
        camy = 30;
    }

    public void AddGold(int value)
    {
        gold += value;
    }
    // Update is called once per frame
    void Update()
    {
        if (book2)
        {
            Vector3 position = new Vector3(transform.position.x - 2f, 3, transform.position.z);
            book2.transform.position = position;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            LockUnlockCamera();
        }

        _forward = Input.GetKey(KeyCode.W);
        _backward = Input.GetKey(KeyCode.S);
        _left = Input.GetKey(KeyCode.A);
        _right = Input.GetKey(KeyCode.D);
        // _jump = Input.GetKey(KeyCode.Space);


       /* if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // CastAbility(0, aimdirection);
            CastAbilitySide(0, aimdirection);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CastAbility1(1, aimdirection);
        }
        if (Input.GetKeyDown(KeyCode.BackQuote) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            CastAbility2(2, aimdirection);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CastAbility3(3, aimdirection);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            CastAbility4(4, aimdirection);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            CastAbility5(5, aimdirection);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            CastAbility6(6, aimdirection);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            CastAbility7(6, aimdirection);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            CastAbility8(8, aimdirection);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            CastAbility9(9, aimdirection);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CastAbility10(10, aimdirection);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            CastAbility11(11, aimdirection);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            CastAbility12(12, aimdirection);
        }
        */

        Vector3 movement = Vector3.zero;
        CamRotation = freeCam.transform.eulerAngles.y;


        if (_forward ^ _backward)
        {
            movement.z += _forward ? 1 : -1;
        }
        if (_left ^ _right)
        {
            movement.x += _right ? 1 : -1;
        }
        movement = Quaternion.Euler(0, CamRotation, 0) * movement;
        movement.Normalize();

        moveDirection1 = movement;

        if (moveDirection1.magnitude > 0.1f)
        {
            moveDirection1.Normalize();

            rotation = Mathf.Atan2(moveDirection1.x, moveDirection1.z) * Mathf.Rad2Deg;
            straight = moveDirection1 * _speed;
        }
        else
        {
            straight = Vector3.zero;
        }
            if (!isImmobilized)
            {//move freely
                if (_isGrounded)
                {
                    rb.velocity = new Vector3(straight.x, rb.velocity.y, straight.z);
                    rb.rotation = Quaternion.Euler(0, rotation, 0);
                }
            }
            else
            {//cant move
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
               // rigidbody.rotation = lockedRotation;
               // rotation = lockedRotation;
            }
        //SmoothRotate(transform, rotation, ref turnSmoothVelocity1, turnSmoothtime1);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 fullsubstraction;
        RaycastHit hit;

        // if (Physics.Raycast(ray, out hit, 100))
        Vector3 spot;
        if (Physics.Raycast(ray, out hit, 100, ignorePlayer))
        {
            fullsubstraction = new Vector3(hit.point.x, hit.point.y, hit.point.z) - transform.position;
            spot = hit.point;
        }
        else
        {
            fullsubstraction = transform.forward;
            spot = transform.position + transform.forward;
        }
        aimdirection = spot;
        targetAngle = Mathf.Atan2(fullsubstraction.x, fullsubstraction.z) * Mathf.Rad2Deg;

        //localdirection = Quaternion.Euler(0, targetAngle, 0) * new Vector3(0, 0, 2f) + transform.position;

    }
    private void LockUnlockCamera()
    {
        ShowCursor = !ShowCursor;
        if (ShowCursor)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            cam1.m_XAxis.m_MaxSpeed = 0;
            cam1.m_YAxis.m_MaxSpeed = 0;

        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            cam1.m_XAxis.m_MaxSpeed = camx;
            cam1.m_YAxis.m_MaxSpeed = camy;
            //Cursor.lockState = CursorLockMode.None;
        }
    }
    public static void SmoothRotate(Transform moveTransform, float rotation, ref float turnSmoothVel, float SmoothTime)
    {
        //var angleDiff = Quaternion.Angle(targetTransform.transform.rotation, moveTransform.rotation);


        float targetAngle = rotation;

        float angle = Mathf.SmoothDampAngle(moveTransform.eulerAngles.y, targetAngle, ref turnSmoothVel, SmoothTime);

        moveTransform.rotation = Quaternion.Euler(0, angle, 0);
    }

    public void CastAbility(int n)
    {
        Instantiate(abilities.abilities[n], transform.position, transform.rotation);
        GameObject instance = Instantiate(abilities.hitboxes[n], transform.position, transform.rotation);

        instance.GetComponent<Damage>().Source = mypowerstats;
    }
    public void CastAbility(int n, Vector3 extradir)
    {
        Instantiate(abilities.abilities[n], extradir, transform.rotation);
        GameObject instance = Instantiate(abilities.hitboxes[n], extradir, transform.rotation);

        instance.GetComponent<Damage>().Source = mypowerstats;
    }
    public void CastAbilitySide(int n, Vector3 extradir)
    {
        Vector3 position = transform.position;
        Quaternion rotation;
        Vector3 fullsubstraction = extradir - transform.position;
        float angle = Mathf.Atan2(fullsubstraction.x, fullsubstraction.z) * Mathf.Rad2Deg;
        rotation = Quaternion.Euler(0, angle, 0);

        Instantiate(abilities.abilities[n], position, rotation);
        GameObject instance = Instantiate(abilities.hitboxes[n], position, rotation);

        instance.GetComponent<Damage>().Source = mypowerstats;
    }
    public void CastAbility1(int n, Vector3 extradir)
    {
        Vector3 position = transform.position;
        Quaternion rotation;
        Vector3 fullsubstraction = extradir - transform.position;
        float angle = Mathf.Atan2(fullsubstraction.x, fullsubstraction.z) * Mathf.Rad2Deg;
        if (ShowCursor)
        {
            rotation = Quaternion.Euler(0, angle, 0);
        }
        else
        {
            // float angle1 = Camera.main.gameObject.transform.forward.;
            Vector3 asdf = Camera.main.gameObject.transform.forward;
            float angle1 = Mathf.Atan2(asdf.x, asdf.z) * Mathf.Rad2Deg;

            rotation = Quaternion.Euler(0, angle1, 0);
        }

        Instantiate(abilities.abilities[n], position, rotation);
        GameObject instance = Instantiate(abilities.hitboxes[n], position, rotation);

        instance.GetComponent<Damage2>().Source = mypowerstats;
    }
    public void CastAbility2(int n, Vector3 extradir)
    {
        Vector3 position = transform.position;
        Quaternion rotation;
        Vector3 fullsubstraction = extradir - transform.position;
        float angle = Mathf.Atan2(fullsubstraction.x, fullsubstraction.z) * Mathf.Rad2Deg;
        if (ShowCursor)
        {
            rotation = Quaternion.Euler(0, angle, 0);
        }
        else
        {
            // float angle1 = Camera.main.gameObject.transform.forward.;
            Vector3 asdf = Camera.main.gameObject.transform.forward;
            float angle1 = Mathf.Atan2(asdf.x, asdf.z) * Mathf.Rad2Deg;

            rotation = Quaternion.Euler(0, angle1, 0);
        }

        Instantiate(abilities.abilities[n], position, rotation);
        GameObject instance = Instantiate(abilities.hitboxes[n], position, rotation);

        instance.GetComponent<Damage2physics>().Source = mypowerstats;
    }
    public void CastAbility3(int n, Vector3 extradir)
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        Instantiate(abilities.abilities[n], position, rotation);
        GameObject instance = Instantiate(abilities.hitboxes[n], position, rotation);

        instance.GetComponent<AreaOfEffect>().Source = mypowerstats;
    }
    public void CastAbility4(int n, Vector3 extradir)
    {
        Vector3 position = transform.position;
        Quaternion rotation;
        Vector3 fullsubstraction = extradir - transform.position;
        float angle = Mathf.Atan2(fullsubstraction.x, fullsubstraction.z) * Mathf.Rad2Deg;
        if (ShowCursor)
        {
            rotation = Quaternion.Euler(0, angle, 0);
        }
        else
        {
            // float angle1 = Camera.main.gameObject.transform.forward.;
            Vector3 asdf = Camera.main.gameObject.transform.forward;
            float angle1 = Mathf.Atan2(asdf.x, asdf.z) * Mathf.Rad2Deg;

            rotation = Quaternion.Euler(0, angle1, 0);
        }

        Instantiate(abilities.abilities[n], position, rotation);
        GameObject instance = Instantiate(abilities.hitboxes[n], position, rotation);

        instance.GetComponent<DamagePhysicsBlast>().Source = mypowerstats;
    }
    public void CastAbility5(int n, Vector3 extradir)
    {
        Instantiate(abilities.abilities[n], extradir, transform.rotation);
        GameObject instance = Instantiate(abilities.hitboxes[n], extradir, transform.rotation);

        instance.GetComponent<Damage>().Source = mypowerstats;
    }

    public void CastAbility6(int n, Vector3 extradir)
    {
        if(books[0] == null)
        {
            Vector3 spawnposition = transform.position + new Vector3(-2, 1 , -1);

            Instantiate(abilities.abilities[n], extradir, transform.rotation);
            GameObject instance = Instantiate(abilities.hitboxes[n], spawnposition, transform.rotation);

            books[0] = instance.GetComponent<SummoningObject>();
            books[0].SetUp(mypowerstats, 4);
        }

        books[0].SetFire(extradir);
    }
    public void CastAbility7(int n, Vector3 extradir)
    {
        if (books[1] == null)
        {
            Vector3 spawnposition = transform.position + new Vector3(2, 1, 1);

            Instantiate(abilities.abilities[n], extradir, transform.rotation);
            GameObject instance = Instantiate(abilities.hitboxes[n], spawnposition, transform.rotation);

            books[1] = instance.GetComponent<SummoningObject>();
            books[1].SetUp(mypowerstats, 7);
        }
        books[1].SetFire(extradir);
    }
    public void CastAbility8(int n, Vector3 extradir)
    {
        Vector3 position = transform.position;
        position.y = 0.1f;
        Quaternion rotation = transform.rotation;

        Vector3 asdf = extradir - transform.position;
        //Quaternion rotation;
        float angle1 = Mathf.Atan2(asdf.x, asdf.z) * Mathf.Rad2Deg;

        rotation = Quaternion.Euler(0, angle1, 0);

        Instantiate(abilities.abilities[n], position, rotation);
        GameObject instance = Instantiate(abilities.hitboxes[n], position, rotation);



        instance.GetComponent<DamageSize>().SetUp(extradir, mypowerstats);
    }
    public void ChangeAngle()
    {
        if(doubleangle1< 45)
        {
            if(doubleangle1 < 20)
            {
                doubleangle1 = 20;
            }
            else
            {
                doubleangle1 = 45;
            }
        }
        else
        {
            doubleangle1 = 0;
        }

        doubleangle.text = doubleangle1.ToString();
    }
    public void CastAbility9(int n, Vector3 extradir)
    {
        Vector3 position = transform.position;
        position.y = 1.4f;
        Quaternion rotation;

        Vector3 asdf = extradir - transform.position;
        //Quaternion rotation;
        float angle1 = Mathf.Atan2(asdf.x, asdf.z) * Mathf.Rad2Deg;

        rotation = Quaternion.Euler(doubleangle1, angle1, 0);

        Instantiate(abilities.abilities[n], position, rotation);
        GameObject instance = Instantiate(abilities.hitboxes[n], position, rotation);

        instance.GetComponent<Damage>().Source = mypowerstats;
    }
    public void CastAbility10(int n, Vector3 extradir)
    {
        if (book2 == null)
        {

        Vector3 position = transform.position;
        position.y = 3f;
        Quaternion rotation;

        Vector3 asdf = extradir - transform.position;
        //Quaternion rotation;
        float angle1 = Mathf.Atan2(asdf.x, asdf.z) * Mathf.Rad2Deg;

        rotation = Quaternion.Euler(doubleangle1, angle1, 0);

        Instantiate(abilities.abilities[n], position, rotation);
        GameObject instance = Instantiate(abilities.hitboxes[n], position, rotation);

            book2 = instance.GetComponent<SummoningObject2>();
            book2.SetUp(mypowerstats, 7);
        //instance.GetComponent<SummoningObject2>().SetUp(mypowerstats, 7);
        }
        book2.SetFire(extradir);
    }
    public void CastAbility11(int n, Vector3 extradir)
    {
        if (book2a == null)
        {
            Vector3 position = transform.position + new Vector3(2, 3, 0);
            position.y = 4f;
            Quaternion rotation;

            Vector3 asdf = extradir - transform.position;
            //Quaternion rotation;
            float angle1 = Mathf.Atan2(asdf.x, asdf.z) * Mathf.Rad2Deg;

            rotation = Quaternion.Euler(doubleangle1, angle1, 0);

            Instantiate(abilities.abilities[n], position, rotation);
            GameObject instance = Instantiate(abilities.hitboxes[n], position, rotation);

            book2a = instance.GetComponent<SummoningObject2a>();
            book2a.SetUp(mypowerstats, 7);
            //instance.GetComponent<SummoningObject2>().SetUp(mypowerstats, 7);
        }
        book2a.SetFire(extradir);
    }
    public void CastAbility12(int n, Vector3 extradir)
    {
        if(book3 == null)
        {

            Instantiate(abilities.abilities[n], transform);
            Vector3 position = transform.position;
            Quaternion rotation = transform.rotation;
            GameObject instance = Instantiate(abilities.hitboxes[n], position, rotation);

            book3 = instance.GetComponent<SummoningObject3>();
            book3.SetUp(mypowerstats, 1);
        }
        else
        {
            book3.SetFire(extradir);

        }
    }

    public void OpenCloseAbilitySets()
    {
        abilitySetsPanel.SetActive(!abilitySetsPanel.activeSelf);
    }
    public void CommandAbility(int n)
    {
        Debug.Log(n);
        if (n == 0)
            CastAbilitySide(n, aimdirection);
        else if (n == 1)
            CastAbility1(n, aimdirection);
        else if (n == 2)
            CastAbility2(n, aimdirection);
        else if (n == 3)
            CastAbility3(n, aimdirection);
        else if (n == 4)
            CastAbility4(n, aimdirection);
        else if (n == 5)
            CastAbility5(n, aimdirection);
        else if (n == 6)
            CastAbility6(n, aimdirection);
        else if (n == 7)
            CastAbility7(6, aimdirection);
        else if (n == 8)
            CastAbility8(n, aimdirection);
        else if (n == 9)
            CastAbility7(n, aimdirection);
        else if (n == 10)
            CastAbility10(n, aimdirection);
        else if (n == 11)
            CastAbility11(n, aimdirection);
        else if (n == 12)
            CastAbility12(n, aimdirection);
    }
}
