using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ObjectControllerConnection : MonoBehaviour
{
    public WebClient webClient;

    public async Awaitable<IWebRequestReponse> ReadObject2Ds(string environmentId)
    {
        string route = "/environments/" + environmentId + "/objects";

        IWebRequestReponse webRequestResponse = await webClient.SendGetRequest(route);
        return ParseObject2DListResponse(webRequestResponse);
    }

    public async Awaitable<IWebRequestReponse> CreateObject2D(ObjectModel object2D)
    {
        string route = "/environments/" + object2D.environmentId + "/objects";
        string data = JsonUtility.ToJson(object2D);

        IWebRequestReponse webRequestResponse = await webClient.SendPostRequest(route, data);
        return ParseObject2DResponse(webRequestResponse);
    }

    public async Awaitable<IWebRequestReponse> UpdateObject2D(ObjectModel object2D)
    {
        string route = "/environments/" + object2D.environmentId + "/objects/" + object2D.id;
        string data = JsonUtility.ToJson(object2D);

        return await webClient.SendPutRequest(route, data);
    }

    private IWebRequestReponse ParseObject2DResponse(IWebRequestReponse webRequestResponse)
    {
        switch (webRequestResponse)
        {
            case WebRequestData<string> data:
                Debug.Log("Response data raw: " + data.Data);
                ObjectModel object2D = JsonUtility.FromJson<ObjectModel>(data.Data);
                WebRequestData<ObjectModel> parsedWebRequestData = new WebRequestData<ObjectModel>(object2D);
                return parsedWebRequestData;
            default:
                return webRequestResponse;
        }
    }

    private IWebRequestReponse ParseObject2DListResponse(IWebRequestReponse webRequestResponse)
    {
        switch (webRequestResponse)
        {
            case WebRequestData<string> data:
                Debug.Log("Response data raw: " + data.Data);
                List<ObjectModel> environments = JsonHelper.ParseJsonArray<ObjectModel>(data.Data);
                WebRequestData<List<ObjectModel>> parsedData = new WebRequestData<List<ObjectModel>>(environments);
                return parsedData;
            default:
                return webRequestResponse;
        }
    }
}