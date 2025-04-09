using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class EnvironmentControllerConnection : MonoBehaviour
{
    public WebClient webClient;

    public async Awaitable<IWebRequestReponse> ReadEnvironment2Ds()
    {
        string route = "/Environment2D";

        IWebRequestReponse webRequestResponse = await webClient.SendGetRequest(route);
        return ParseEnvironment2DListResponse(webRequestResponse);
    }

    public async Awaitable<IWebRequestReponse> CreateEnvironment(EnvironmentModel environment)
    {
        string route = "/Environment2D";
        string data = JsonUtility.ToJson(environment);

        IWebRequestReponse webRequestResponse = await webClient.SendPostRequest(route, data);
        return ParseEnvironment2DResponse(webRequestResponse);
    }

    public async Awaitable<IWebRequestReponse> DeleteEnvironment(string environmentId)
    {
        string route = "/Environment2D/" + environmentId;
        return await webClient.SendDeleteRequest(route);
    }

    private IWebRequestReponse ParseEnvironment2DResponse(IWebRequestReponse webRequestResponse)
    {
        switch (webRequestResponse)
        {
            case WebRequestData<string> data:
                Debug.Log("Response data raw: " + data.Data);
                EnvironmentModel environment = JsonUtility.FromJson<EnvironmentModel>(data.Data);
                WebRequestData<EnvironmentModel> parsedWebRequestData = new WebRequestData<EnvironmentModel>(environment);
                return parsedWebRequestData;
            default:
                return webRequestResponse;
        }
    }

    private IWebRequestReponse ParseEnvironment2DListResponse(IWebRequestReponse webRequestResponse)
    {
        switch (webRequestResponse)
        {
            case WebRequestData<string> data:
                Debug.Log("Response data raw: " + data.Data);
                List<EnvironmentModel> environment2Ds = JsonHelper.ParseJsonArray<EnvironmentModel>(data.Data);
                WebRequestData<List<EnvironmentModel>> parsedWebRequestData = new WebRequestData<List<EnvironmentModel>>(environment2Ds);
                return parsedWebRequestData;
            default:
                return webRequestResponse;
        }
    }

}

