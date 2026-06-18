using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
/*using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;*/

public class Player : KitchenObjectHolder
{
    public static Player Instance { get; private set; }
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotatoSpeed=6f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterlayerMask;

    private bool isWalik = false;
    private BaseCounter selectdeCounter;

    // Start iss called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnOprateAction += GameInput_OnOprateAction;
       print("Player Start");
    }



    // Update is called once per frame
    void Update()
    {
        HandleInteraction();
    }



    private void FixedUpdate()
    {
        HaldleMovement();
    }



    public bool IsWalking
    {
        get
        {
            return isWalik;
        }
       
    }



    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        selectdeCounter?.Interact(this);
    }
    private void GameInput_OnOprateAction(object sender, System.EventArgs e)
    {
        if (selectdeCounter == null) print("选中柜台为空，无法执行 Operate");
        selectdeCounter?.InteractOperate(this);
    }




    private void HaldleMovement()
    {
        Vector3 direction = gameInput.GetMovementDirectionNormalized();
        isWalik = direction != Vector3.zero;
        transform.position += direction * Time.deltaTime * speed;

        if (direction != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime * rotatoSpeed);
        }
    }



    private void HandleInteraction()
    {
  
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitinfo, 2f, counterlayerMask))
        {
             
            if (hitinfo.transform.TryGetComponent<BaseCounter>(out BaseCounter counter))
            
            {
                print("选中柜台：" + counter.name);
                SetSelectedCounter(counter);
            }
            else
            {
                SetSelectedCounter(null);
            }
           
        }
        else
        {
            SetSelectedCounter(null);
        }
    }



    public void SetSelectedCounter(BaseCounter counter)
    {
        if(counter!=selectdeCounter)
        {
            selectdeCounter?.CancelSelect();
            counter?.SelectCounter();
            this.selectdeCounter = counter;
        }
       
    }
}
