﻿namespace FSH.WebApi.Application.DummyJobs;
public class JobTestService : IJobTestService
{
    public void ContinuationJob()
    {
        Console.WriteLine("Hello from a Continuation job!");
    }

    public void DelayedJob()
    {
        Console.WriteLine("Hello from a Delayed job!");
    }

    public void FireAndForgetJob()
    {
        Console.WriteLine("Hello from a Fire and Forget job!");
    }

    public void ReccuringJob()
    {
        Console.WriteLine("Hello from a Scheduled job!");
    }
}