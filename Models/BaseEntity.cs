using System;

namespace FloriSys.Models
{
    /// <summary>
    /// Abstract base class for all entity models.
    /// Demonstrates: ABSTRACTION (abstract class, abstract property),
    /// POLYMORPHISM (virtual methods that subclasses override),
    /// ENCAPSULATION (IsValid validation logic encapsulated in model).
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Every entity provides a human-readable display text.
        /// ABSTRACTION: subclasses MUST implement this.
        /// </summary>
        public abstract string DisplayText { get; }

        /// <summary>
        /// Common validation logic.
        /// POLYMORPHISM: virtual so subclasses can override with custom rules.
        /// </summary>
        public virtual bool IsValid => !string.IsNullOrEmpty(DisplayText);

        /// <summary>
        /// Returns the entity's primary key value.
        /// ABSTRACTION: each entity knows its own ID.
        /// </summary>
        public abstract string Id { get; }
    }
}
