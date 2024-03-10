using System.Collections.Generic;
using UnityEngine;

public class FlyingWeightFactory : MonoBehaviour
{
    static FlyingWeightFactory _instance;
    public List<FlyingWeightFactoryStruct> pools;
    
    private void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space))
        {
            FlyWeight newFlyWeight = GetFullPool(FlyWeightType.ice).SpawObject();
            newFlyWeight.gameObject.SetActive(true);
        }
    }

    private void Awake()
    {
        if (_instance != this && _instance != null)
        {
            Destroy(_instance);
        }
        else
        {
            _instance = this;
        }
    }

    public IObjectPool<FlyWeight> GetFullPool(FlyWeightType _flyWeightType)
    {
        IObjectPool<FlyWeight> newPool = null;

        for (int i = 0; i < pools.Count; i++)
        {
            if (pools[i].flyHeightType == _flyWeightType)
            {
                newPool = pools[i].pool;
            }
        }
   
        return newPool;
    }


}
[System.Serializable]
public struct FlyingWeightFactoryStruct
{
    public FlyWeightType flyHeightType;
    public FlyingWeightPool pool;

}
public enum FlyWeightType
{
    fire = 0,
    ice = 1,
}
