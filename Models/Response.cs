﻿namespace Bank.Management.Api;

public class Response
{
    public string RequestId => $"{Guid.NewGuid().ToString()}";
    public string ResponseCode { get; set; }
    public string ResponseMessage { get; set; }
    public object Date { get; set; }
}
