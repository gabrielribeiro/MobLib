using MobLib.Core.Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobLib.Core.Domain.Entities
{
    public abstract class BaseEntity : IEntity
    {
        public BaseEntity() { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
