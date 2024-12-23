using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityObject = UnityEngine.Object;

namespace ACG
{
    public static class AddressModel
    {
        private const string TAG = "AddressModel";
        public static T LoadAsset<T>(string addr) where T : UnityObject
        {
            try
            {
                var asyncHandle = Addressables.LoadAsset<T>(addr);
                asyncHandle.WaitForCompletion();

                return asyncHandle.Result;
            }
            catch(Exception ex)
            {
                LogUtil.e(TAG, ex.ToString());
            }

            return null;
        }

        public static void LoadAssetAsync<T>(string addr, Action<T, string> callBack) where T : UnityObject
        {
            try
            {
                var asyncHandle = Addressables.LoadAssetAsync<T>(addr);

                asyncHandle.Completed += (AsyncOperationHandle<T> handle) => {
                    if (handle.IsDone && handle.Result != null)
                    {
                        callBack?.Invoke(handle.Result, addr);
                    }
                    else
                    {
                        LogUtil.e(TAG, $"LoadAssetAsync failed: {addr}");
                        callBack?.Invoke(null, addr);
                    }
                };
            }
            catch(Exception ex)
            {
                LogUtil.e(TAG, $"LoadAssetAsync exception: {ex.ToString()}");
                callBack?.Invoke(null, addr);
            }

        }

        public static void LoadAssetsAsync<T>(IList<string> keys, Action<IList<T>, IList<string>> callback)
            where T : UnityObject
        {
            try
            {
                var asyncHandle = Addressables.LoadAssetsAsync<T>(keys, (_)=>{}, Addressables.MergeMode.Union, false);
                asyncHandle.Completed += (handle) =>
                {
                    //if (handle is { IsDone: true, Result: not null })
                    if (((AsyncOperationHandle<IList<T>>)handle).IsDone == true && ((AsyncOperationHandle<IList<T>>)handle).Result != null)
                    {
                        callback?.Invoke(handle.Result, keys);
                    }
                    else
                    {
                        LogUtil.e(TAG, $"LoadAssetsAsync failed");
                    }
                };
            }
            catch (Exception ex)
            {
                LogUtil.e(TAG, $"LoadAssetAsync exception: {ex.ToString()}"); 
            }
        }
    }
}