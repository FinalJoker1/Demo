using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomerAgent : MonoBehaviour
{
    public ShowOrder showOrder;
    public Menu menu;
    public int maxLink = 3;
    private HashSet<Client> list = new HashSet<Client>();
    private Coroutine checkCorotine;
    public float checkTime = 3;

    public void Start()
    {
        showOrder = AssetsLoader.showOrder[1];
    }
    public void EnableAgent()
    {
        checkCorotine = StartCoroutine(Check());
    }

    public void DisableAgent()
    {
        StopCoroutine(checkCorotine);
        checkCorotine = null;
    }

    private IEnumerator Check()
    {
        while(true)
        {
            yield return new WaitForSeconds(checkTime);
            int currentCount = list.Count;
            for (int i = 0; i < maxLink - currentCount; i++)
                AutoOrder();
        }
    }


    public void AutoOrder()
    {
        if (list.Count >= maxLink)
            return;

        int count = Random.Range(0, menu.productFrameList.Count + 1);

        if (count == 0)
            return;

        Client client = new Client(AssetsLoader.customers[Random.Range(0, AssetsLoader.customers.Count)], showOrder, menu);
        list.Add(client);
        client.OrderDone += OrderDoneCallBack;
        
        for (int i = 0; i < count; i++)
            client.Add(menu.productFrameList[i].GetProduct());

        client.Submit();
    }

    private void OrderDoneCallBack(object client)
    {
        list.Remove(client as Client);
    }
}
