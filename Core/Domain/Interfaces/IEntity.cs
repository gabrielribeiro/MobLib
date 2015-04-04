using System;

namespace MobLib.Core.Domain.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }
        bool Active { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
    }
}
