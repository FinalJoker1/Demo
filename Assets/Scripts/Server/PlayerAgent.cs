using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAgent : MonoBehaviour
{
    public enum State
    {
        Idle = 1,
        Doing = 2,
        Holding = 4
    }

    public bool HaveState(State state)
    {
        return (PlayerState & state) == state;
    }

    public void RemoveState(State state)
    {
        PlayerState &= (~state);
    }

    public void AddState(State state)
    {
        PlayerState |= state;
    }

    
    public IWorkbench<IRawMaterial> Workbench { get; private set; }
    private OrderDisplayFrame waitForMakeProduct;
    public GameObject CurrentHoldRawMaterialGO { get; private set; }
    public Transform holdPoint;
    public ProductUnpassedUI productUnpassedUI;
    private Camera playerCamera;
    private Money _property = new Money(0);
    public ICurrency Property { get => _property; }
    public State PlayerState { get; private set; }

    private Queue<Coroutine> _coroutineQueue = new Queue<Coroutine>();
    //public Coroutine ChooseRawMaterialCoroutine { get; private set; }
    private bool _startLock = false;

    public string rawMaterialTag = "RawMaterial";
    public string workbenchTag = "WorkBench";
    public int linkShowUIId = 1;
    public float maxRayDistance;
    private MouseAgent _mouseAgent;

    public void InitProperty(object property)
    {
        _property = property as Money;
    }
    public void Awake()
    {
        playerCamera = GetComponent<Camera>();
        _mouseAgent = GetComponent<MouseAgent>();
        AssetsLoader.player = this;
        AssetsLoader.playerMouseAgent = _mouseAgent;  
    }

    public void Start()
    {
        PlayerState = State.Idle;
        Cursor.lockState = CursorLockMode.Locked;
    }


    public void StartMake(OrderDisplayFrame waitForMakeProduct)
    {
        if (HaveState(State.Doing) || _startLock)
            return;
        _startLock = true;
        //从waitForMakeProduct中拿到此产品所属的工作台对象id
        IWorkbench<IRawMaterial> tryGetWorkbench;
        AssetsLoader.workbenchs.TryGetValue(waitForMakeProduct.product.linkWorkbenchID,out tryGetWorkbench);

        if (tryGetWorkbench == null)
        {
            _startLock = false;
            return;
        }
            

        Workbench = tryGetWorkbench;

        this.waitForMakeProduct = waitForMakeProduct;
        AddState(State.Doing);
        Workbench.StartDo();
        _coroutineQueue.Enqueue(StartCoroutine(TryChooseMaterial()));
        _startLock = false;
    }


    private IEnumerator TryChooseMaterial()
    {
        GameObject hitGO = null;
        while (HaveState( State.Doing))
        {
            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f,0.5f,0f));
            RaycastHit hit;
            bool isHit = Physics.Raycast(ray, out hit, maxRayDistance);

            if(isHit)
            {
                if (hitGO != null)
                {
                    if (hitGO.Equals(hit.collider.gameObject) == false)
                    {
                        hitGO.GetComponent<ISelect>()?.Unselect();
                        hit.collider.gameObject.GetComponent<ISelect>()?.Select();
                    }
                }
                else
                {
                    hit.collider.gameObject.GetComponent<ISelect>()?.Select();
                }

                hitGO = hit.collider.gameObject;
            }
            else
            {
                if(hitGO != null)
                {
                    hitGO.GetComponent<ISelect>()?.Unselect();
                }
                hitGO = null;
            }

            if (isHit && _mouseAgent.GetMouseButtonDown(0))
            {
                if (hit.collider.gameObject.tag == rawMaterialTag)
                {
                    CurrentHoldRawMaterialGO = hit.collider.gameObject;
                    var rawMaterial = CurrentHoldRawMaterialGO.GetComponent<RawMaterialMono>()?.Material;
                    var holdCoroutine = StartCoroutine(Hold(CurrentHoldRawMaterialGO.GetComponent<IGOTransformAgent>(), rawMaterial));
                    _coroutineQueue.Enqueue(holdCoroutine);
                    yield return holdCoroutine;
                }
                //Workbench.GetRawMaterial(hit.collider.gameObject.GetComponent<RawMaterialMono>()?.Material);

                // Debug.DrawRay(ray.origin, hit.point, Color.red);
            }
            yield return null;

        }

        yield return null;  
    }

    private IEnumerator Hold(IGOTransformAgent transformAgent,IRawMaterial rawMaterial)
    {
        if (transformAgent == null)
            yield return null;

        AddState(State.Holding);
        transformAgent.SetParent(holdPoint);
        while (HaveState(State.Holding))
        {
            if(_mouseAgent.GetMouseButtonDown(1))
            {
                RemoveState(State.Holding);
            }
            else
            {
                //var mousePosition = _mouseAgent.mousePosition;
                Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f,0.5f,0f));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, maxRayDistance) && _mouseAgent.GetMouseButtonDown(0))
                {
                    if(hit.collider.tag == workbenchTag)
                    {
                        var catchWorkbench = hit.collider.GetComponent<IWorkbench<IRawMaterial>>();
                        if(catchWorkbench.GetWorkbenchID == Workbench.GetWorkbenchID)
                        {
                            Workbench.GetRawMaterial(rawMaterial);
                            
                            RemoveState(State.Holding);
                        }
                    }
                }
            }

            yield return null;
        }
        transformAgent.Reset();
        yield return null;
    }
    public void StopMake()
    {

        ResetSelf();
    }

    private void ResetSelf()
    {
        while(_coroutineQueue.Count > 0)
            StopCoroutine(_coroutineQueue.Dequeue());

        this.waitForMakeProduct = null;
        PlayerState = State.Idle;
        CurrentHoldRawMaterialGO?.GetComponent<IGOTransformAgent>()?.Reset();
        CurrentHoldRawMaterialGO = null;
        Workbench?.MixRawMaterial();
        Workbench = null;
    }
    public void Finish()
    {
        if (HaveState(State.Doing) == false || Workbench == null)
            return;

        Workbench.MixRawMaterial();
        var res = Workbench.Output() as Product;

        res = res == null ? AssetsLoader.products.DefaultValue : res;

        //print("make result:" + res.id);
        ISeller seller = new Seller(res, new Money(res.price), (returnProperty) => _property.PlusCurrency(returnProperty));
        //让玩家与选择的Order进行交易
        var selectFrame = waitForMakeProduct.frame;
        /**
         * 回调传一个临时变量让委托闭包捕获，不要传当前实例的成员变量，因为多线程是非阻塞的，
         * 之后的代码可能修改当前实例的成员变量导致回调出问题
        **/
        AssetsLoader.trader?.StartTrade(seller, waitForMakeProduct,()=> Destroy(selectFrame),() => productUnpassedUI?.ShowUI());

        ResetSelf();
    }

    
}

public class Seller : ISeller
{
    private IProduct output;
    private ICurrency expectedReturn;
    public Action Unpassed => null;
    private Action<ICurrency> GetCurrency;

    public Seller(IProduct output, ICurrency expectedReturn,Action<ICurrency> GetCurrency = null)
    {
        this.output = output;
        this.expectedReturn = expectedReturn;
        this.GetCurrency = GetCurrency;
    }

    public bool Check(ICurrency currency)
    {

        if( expectedReturn.Equals(currency))
        {
            GetCurrency?.Invoke(currency);
            return true;
        }

        return false;
    }

    public IProduct GetProduct()
    {
        return output;
    }

    public ICurrency Need()
    {
        return expectedReturn;
    }
}


