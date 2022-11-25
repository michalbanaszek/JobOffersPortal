using JobOffersPortal.Domain.Common;
using System;

namespace JobOffersPortal.Domain.Primitives
{
    public abstract class Entity : AuditableEntity, IEquatable<Entity>
    {
        protected Entity(string id)
        {
            Id = id;
        }

        protected Entity()
        { }

        public string Id { get; private init; }

        public static bool operator ==(Entity? first, Entity? second)
        {
            return first is not null && second is not null && first.Equals(second);
        }

        public static bool operator !=(Entity? first, Entity? second)
        {
            return !(first == second);
        }

        public bool Equals(Entity other)
        {
            if (other is null)
            {
                return false;
            }

            if (other.GetType() != this.GetType())
            {
                return false;
            }

            return other.Id == this.Id;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            if (obj is not Entity entity)
            {
                return false;
            }

            return entity.Id == this.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
