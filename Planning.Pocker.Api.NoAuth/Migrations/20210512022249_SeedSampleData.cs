using Bogus;
using Microsoft.EntityFrameworkCore.Migrations;
using Planning.Pocker.Api.NoAuth.Model;
using System;
using System.Linq;

namespace Planning.Pocker.Api.NoAuth.Migrations
{
    public partial class SeedSampleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Sample data!!!
            var random = new Random();
            var faker = new Faker();
            var cartas = "".PadLeft(random.Next(7, 10)).Select(_ => new Carta { Id = Guid.NewGuid(), Valor = random.Next(1, 10) }).ToList();
            var historias = "".PadLeft(random.Next(5, 15)).Select(_ => new Historial { Id = Guid.NewGuid(), Descripcion = faker.Lorem.Sentence(3) }).ToList();
            var usuarios = "".PadLeft(random.Next(10, 20)).Select(_ => new Usuario { Id = Guid.NewGuid(), Nombre = faker.Name.FullName() }).ToList();
            var votos = "".PadLeft(random.Next(5, 17)).Select(_ => new Voto
            {
                Id = Guid.NewGuid(),
                Carta = cartas[random.Next(cartas.Count)],
                Historial = historias[random.Next(historias.Count)],
                Usuario = usuarios[random.Next(usuarios.Count)]
            }).ToList();

            foreach (var carta in cartas)
                migrationBuilder.Sql($@"INSERT INTO [{nameof(Carta)}] ([{nameof(Carta.Id)}], [{nameof(Carta.Valor)}]) VALUES ('{carta.Id}', {carta.Valor})");
            foreach (var historia in historias)
                migrationBuilder.Sql($@"INSERT INTO [{nameof(Historial)}] ([{nameof(Historial.Id)}], [{nameof(Historial.Descripcion)}]) VALUES ('{historia.Id}', '{historia.Descripcion}')");
            foreach (var usuario in usuarios)
                migrationBuilder.Sql($@"INSERT INTO [{nameof(Usuario)}] ([{nameof(Usuario.Id)}], [{nameof(Usuario.Nombre)}]) VALUES ('{usuario.Id}', '{usuario.Nombre}')");
            foreach (var voto in votos)
                migrationBuilder.Sql($@"INSERT INTO [{nameof(Voto)}] ([{nameof(Voto.Id)}], [{nameof(Voto.CartaId)}], [{nameof(Voto.HistorialId)}], [{nameof(Voto.UsuarioId)}])" +
                    $@" VALUES ('{voto.Id}', '{voto.Carta.Id}', '{voto.Historial.Id}', '{voto.Usuario.Id}')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"DELETE FROM [{nameof(Voto)}]");
            migrationBuilder.Sql($@"DELETE FROM [{nameof(Usuario)}]");
            migrationBuilder.Sql($@"DELETE FROM [{nameof(Historial)}]");
            migrationBuilder.Sql($@"DELETE FROM [{nameof(Carta)}]");
        }
    }
}
