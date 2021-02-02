﻿namespace Harry.Data.Entities
{
    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }

        bool IsTransient();
    }
}
