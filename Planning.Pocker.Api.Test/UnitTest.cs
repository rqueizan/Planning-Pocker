using Planning.Pocker.Api.NoAuth.Handlers;
using System;
using Xunit;

namespace Planning.Pocker.Api.Test
{
    public class UnitTest
    {
        private const string stringNull = null;
        private const string stringEmpty = "";
        private const string stringWhiteSpace = " ";
        private const string stringWhiteSpaces = "  ";
        private const string stringValid = "SomeText";

        [Theory]
        [InlineData(int.MinValue, false)]
        [InlineData(-1, false)]
        [InlineData(0, false)]
        [InlineData(2, false)]
        [InlineData(3, true)]
        [InlineData(5, true)]
        [InlineData(10, true)]
        [InlineData(11, false)]
        [InlineData(int.MaxValue, false)]

        public void Test_CreateCartaValidator(int valor, bool valid)
        {
            var command = new CreateCartaCommand { Valor = valor };
            var validator = new CreateCartaValidator();
            var result = validator.Validate(command);
            Assert.Equal(valid, result.IsValid);
        }

        [Theory]
        [InlineData(int.MinValue, false)]
        [InlineData(-1, false)]
        [InlineData(0, false)]
        [InlineData(2, false)]
        [InlineData(3, true)]
        [InlineData(5, true)]
        [InlineData(10, true)]
        [InlineData(11, false)]
        [InlineData(int.MaxValue, false)]

        public void Test_UpdateCartaValidator(int valor, bool valid)
        {
            var command = new UpdateCartaCommand { Id = Guid.NewGuid(), Valor = valor };
            var validator = new UpdateCartaValidator();
            var result = validator.Validate(command);
            Assert.Equal(valid, result.IsValid);
        }

        [Theory]
        [InlineData(stringNull, false)]
        [InlineData(stringEmpty, false)]
        [InlineData(stringWhiteSpace, false)]
        [InlineData(stringWhiteSpaces, false)]
        [InlineData(stringValid, true)]

        public void Test_CreateHistorialValidator(string descripcion, bool valid)
        {
            var command = new CreateHistorialCommand { Descripcion = descripcion };
            var validator = new CreateHistorialValidator();
            var result = validator.Validate(command);
            Assert.Equal(valid, result.IsValid);
        }

        [Theory]
        [InlineData(stringNull, false)]
        [InlineData(stringEmpty, false)]
        [InlineData(stringWhiteSpace, false)]
        [InlineData(stringWhiteSpaces, false)]
        [InlineData(stringValid, true)]

        public void Test_UpdateHistorialValidator(string descripcion, bool valid)
        {
            var command = new UpdateHistorialCommand { Id = Guid.NewGuid(), Descripcion = descripcion };
            var validator = new UpdateHistorialValidator();
            var result = validator.Validate(command);
            Assert.Equal(valid, result.IsValid);
        }

        [Theory]
        [InlineData(stringNull, false)]
        [InlineData(stringEmpty, false)]
        [InlineData(stringWhiteSpace, false)]
        [InlineData(stringWhiteSpaces, false)]
        [InlineData(stringValid, true)]

        public void Test_CreateUsuarioValidator(string nombre, bool valid)
        {
            var command = new CreateUsuarioCommand { Nombre = nombre };
            var validator = new CreateUsuarioValidator();
            var result = validator.Validate(command);
            Assert.Equal(valid, result.IsValid);
        }

        [Theory]
        [InlineData(stringNull, false)]
        [InlineData(stringEmpty, false)]
        [InlineData(stringWhiteSpace, false)]
        [InlineData(stringWhiteSpaces, false)]
        [InlineData(stringValid, true)]

        public void Test_UpdateUsuarioValidator(string nombre, bool valid)
        {
            var command = new UpdateUsuarioCommand { Id = Guid.NewGuid(), Nombre = nombre };
            var validator = new UpdateUsuarioValidator();
            var result = validator.Validate(command);
            Assert.Equal(valid, result.IsValid);
        }
    }
}
