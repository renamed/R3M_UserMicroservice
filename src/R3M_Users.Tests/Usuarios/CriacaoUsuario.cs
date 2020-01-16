using Xunit;
using R3M_User_Service;
using R3M_User_Domain.Apoio;
using R3M_User_Domain;
using System.Threading.Tasks;
using System;
using R3M_User_ExternalServices.Interfaces;
using NSubstitute;

namespace R3M_Users.Tests.Usuarios
{
    public class CriacaoUsuario
    {
        [Fact]
        public async Task UsuarioNull()
        {
            var usuarioService = new UsuarioService(null, null);
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await usuarioService.AdicionarUsuario(null));

            Assert.Equal("Usuário não informado", exception.Message);
        }

        [Fact]
        public async Task EmailNull()
        {
            var usuarioService = new UsuarioService(null, null);
            var usuario = new Usuario();
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await usuarioService.AdicionarUsuario(usuario));

            Assert.Equal("Email não é válido", exception.Message);
        }

        [Fact]
        public async Task EmailNaoValido()
        {
            var usuarioService = new UsuarioService(null, null);
            var usuario = new Usuario()
            {
                Email = "naoevalido"
            };
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await usuarioService.AdicionarUsuario(usuario));

            Assert.Equal("Email não é válido", exception.Message);
        }

        [Fact]
        public async Task NomeInvalido()
        {
            var usuarioService = new UsuarioService(null, null);
            var usuario = new Usuario()
            {
                Email = "email@valido.com"
            };
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await usuarioService.AdicionarUsuario(usuario));

            Assert.Equal("Nome não informado", exception.Message);
        }

        [Fact]
        public async Task DataNascimentoNull()
        {
            var usuarioService = new UsuarioService(null, null);
            var usuario = new Usuario()
            {
                Email = "email@valido.com",
                Nome = "Fulano da Silva"
            };
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await usuarioService.AdicionarUsuario(usuario));

            Assert.Equal("Data de nascimento não é válida", exception.Message);
        }

        [Fact]
        public async Task DataNascimentoSupHoje()
        {
            var usuarioService = new UsuarioService(null, null);
            var usuario = new Usuario()
            {
                Email = "email@valido.com",
                Nome = "Fulano da Silva",
                Nascimento = DateTime.UtcNow.AddDays(1)
            };
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await usuarioService.AdicionarUsuario(usuario));

            Assert.Equal("Data de nascimento não é válida", exception.Message);
        }

        [Fact]
        public async Task SenhaNull()
        {
            var usuarioService = new UsuarioService(null, null);
            var usuario = new Usuario()
            {
                Email = "email@valido.com",
                Nome = "Fulano da Silva",
                Nascimento = DateTime.UtcNow.AddYears(-30)
            };
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await usuarioService.AdicionarUsuario(usuario));

            Assert.Equal("Senha não informada", exception.Message);
        }

        [Fact]
        public async Task SenhaFraca()
        {
            var usuarioService = new UsuarioService(null, null);
            var usuario = new Usuario()
            {
                Email = "email@valido.com",
                Nome = "Fulano da Silva",
                Nascimento = DateTime.UtcNow.AddYears(-30),
                Senha = "a12"
            };
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await usuarioService.AdicionarUsuario(usuario));

            Assert.Equal("Senha muito fraca", exception.Message);
        }

        [Fact]
        public async Task EmailDuplicado()
        {
            var usuarioExternalService = Substitute.For<IUsuarioExternalService>();
            usuarioExternalService.GetByEmail(Arg.Is<string>("email@valido.com")).Returns(new Usuario { });

            var usuarioService = new UsuarioService(usuarioExternalService, null);
            var usuario = new Usuario()
            {
                Email = "email@valido.com",
                Nome = "Fulano da Silva",
                Nascimento = DateTime.UtcNow.AddYears(-30),
                Senha = "a123"
            };
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await usuarioService.AdicionarUsuario(usuario));

            Assert.Equal("Usuário já cadastrado", exception.Message);
        }

        [Fact]
        public async Task AddSucesso()
        {
            var usuarioExternalService = Substitute.For<IUsuarioExternalService>();
            usuarioExternalService.GetByEmail(Arg.Is<string>("email@valido.com"));
            usuarioExternalService.Adicionar(Arg.Any<Usuario>()).Returns(x => new Usuario { IdUsuario = 123 });

            var usuarioService = new UsuarioService(usuarioExternalService, null);
            var usuario = new Usuario()
            {
                Email = "email@valido.com",
                Nome = "Fulano da Silva",
                Nascimento = DateTime.UtcNow.AddYears(-30),
                Senha = "a123"
            };
            var novoUsuario = await usuarioService.AdicionarUsuario(usuario);

            Assert.Equal(123, novoUsuario.IdUsuario);
        }
    }
}