using System.ComponentModel;

namespace rate_limit_by_user_access_level.Domain;

public enum AccessLevel
{
    [Description("Free")] Free,
    [Description("Basic")] Basic,
    [Description("Pro")] Pro,
    [Description("Enterprise")] Enterprise
}
