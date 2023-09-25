using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ApiTests;

public class IntegrationTest :WebApplicationFactory<Program>, IDisposable
{
    private HttpClient? _httpClient;

    protected HttpClient HttpClient
    {
        get
        {
            if (_httpClient == null)
                _httpClient = CreateDefaultClient();

            return _httpClient;
        }
    }
    
    public void Dispose()
    {
        HttpClient.Dispose();
        base.Dispose();
    }
}

