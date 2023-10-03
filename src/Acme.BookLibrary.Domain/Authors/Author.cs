using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookLibrary.Authors;

public class Author : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; set; }
    public string ShortBio { get; set; }

    private Author()
    {
        /* This constructor is for deserialization / ORM purpose */
    }

    internal Author(
        Guid id,
        [NotNull] string name,
        [CanBeNull] string shortBio = null)
        : base(id)
    {
        SetName(name);
        ShortBio = shortBio;
    }

    internal Author ChangeName([NotNull] string name)
    {
        SetName(name);
        return this;
    }

    private void SetName([NotNull] string name)
    {
        Name = Check.NotNullOrWhiteSpace(
            name,
            nameof(name),
            maxLength: 150
        );
    }
}