using System;

namespace Company.Data.Interfaces
{
    public interface IEntity : IModifiableEntity
    {
        int Id { get; set; }
        DateTime Created { get; set; }
        DateTime? Modified { get; set; }
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
        bool IsDeleted { get; set; }
    }

    //public interface IEntity<T> : IEntity
    //{
    //    new T Id { get; set; }
    //}
}
