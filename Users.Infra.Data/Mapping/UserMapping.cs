using Users.Domain.Users.Entities;
using Users.Generics.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Users.Infra.Data.Mapping
{
    public class UserMapping : EntityTypeConfiguration<Usuario>
    {
        public override void Map(EntityTypeBuilder<Usuario> builder)
        {
            builder.Property(_ => _.Email)
                   .IsRequired();

            builder.Property(_ => _.Senha)
                   .IsRequired();

            builder.Property(_ => _.Nome)
                   .IsRequired();

            builder.Ignore(_ => _.CascadeMode);
            builder.Ignore(_ => _.ValidationResult);

            builder.ToTable("Usuarios");
        }
    }
}
