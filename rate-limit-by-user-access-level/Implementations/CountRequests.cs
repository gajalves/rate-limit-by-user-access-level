using rate_limit_by_user_access_level.Abstractions;

namespace rate_limit_by_user_access_level.Implementations;

public class CountRequests : ICountRequests
{
    private int _count = 0;
    public int Count()
    {
        return ++_count;
    }
}
