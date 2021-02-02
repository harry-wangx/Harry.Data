using Harry.Data.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Data.Entities
{

    [Serializable]
    public abstract class AuditedEntity<TPrimaryKey, TUserKey> : Entity<TPrimaryKey>, IAudited<TUserKey>
    {
        public TUserKey CreatorId { get; set; }
        public string CreatorName { get; set; }
        public DateTime CreationTime { get; set; }
        public TUserKey LastModifierId { get; set; }
        public string LastModifierName { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public TUserKey DeleterId { get; set; }
        public string DeleterName { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }

    [Serializable]
    public abstract class AuditedEntity<TPrimaryKey, TUser, TUserKey> : AuditedEntity<TPrimaryKey, TUserKey>, IAudited<TUser, TUserKey>
        where TUser : IEntity<TUserKey>
    {

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("CreatorId")]
        public TUser Creator { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("LastModifierId")]
        public TUser LastModifier { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("DeleterId")]
        public TUser Deleter { get; set; }
    }
}
