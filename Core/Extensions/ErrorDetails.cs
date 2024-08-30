using System;
using Newtonsoft.Json;

namespace Core.Extensions;

public class ErrorDetails
{
    public string Message { get; set; }
    public int StatusCode { get; set; }

    override public string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}