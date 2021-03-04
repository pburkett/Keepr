using System;

namespace keepr_server.Exceptions
{
  public class NotAuthorized : Exception
  {
    public NotAuthorized(string message) : base(message)
    {
    }
  }
}