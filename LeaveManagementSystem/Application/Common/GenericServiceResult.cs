using System;

namespace LeaveManagementSystem.Application.Common;

public class GenericServiceResult<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; }
    public string? Error { get; set; }

    public static GenericServiceResult<T> Fail(string error) => new() { Success = false, Error = error };
    public static GenericServiceResult<T> Ok(T data) => new() { Success = true, Data = data };
}

