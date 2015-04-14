using System;

namespace MobLib.Core.Domain.Interfaces
{
    public interface IMobEntity
    {
        int Id { get; set; }
        bool Active { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
    }
}
