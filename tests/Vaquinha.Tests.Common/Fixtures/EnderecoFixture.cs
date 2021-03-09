using Bogus; //Server para gerar os fakes
using Bogus.DataSets; //Ele gera dados pra gente sabendo que são dados validos
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Vaquinha.Domain.Entities;
using Vaquinha.Domain.ViewModels;

namespace Vaquinha.Tests.Common.Fixtures
{
    [CollectionDefinition(nameof(EnderecoFixtureCollection))]
    public class EnderecoFixtureCollection : ICollectionFixture<EnderecoFixture>
    {
    }
    public class EnderecoFixture
    {
        //Endereço model: Vamos gerar um endereço view model valido
        public EnderecoViewModel EnderecoModelValido()
        {
            var endereco = new Faker().Address; //Gera um endereço valido pra gente

            var faker = new Faker<EnderecoViewModel>("pt_BR");

            faker.RuleFor(c => c.CEP, (f, c) => "14800-700");
            faker.RuleFor(c => c.Cidade, (f, c) => endereco.City());
            faker.RuleFor(c => c.Estado, (f, c) => endereco.StateAbbr());
            faker.RuleFor(c => c.TextoEndereco, (f, c) => endereco.StreetAddress());            

            return faker.Generate();
        }


        //São cenários que podem dar problemas e outros que não dão problemas, Caso de uso
        public Endereco EnderecoValido()//Verifica se mu metodo é valido
        {
            var endereco = new Faker("pt_BR").Address;
            
            var faker = new Faker<Endereco>("pt_BR");

            faker.CustomInstantiator(f =>
                 new Endereco(Guid.NewGuid(), "14800-000", endereco.StreetAddress(false), string.Empty, endereco.City(), endereco.StateAbbr(), "16995811385", "100A"));

            return faker.Generate();
        }

        public Endereco EnderecoVazio()
        {
            return new Endereco(Guid.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        public Endereco EnderecoCepTelefoneEstadoInvalido()
        {
            var endereco = new Faker("pt_BR").Address;

            var faker = new Faker<Endereco>("pt_BR");

            faker.CustomInstantiator(f =>
                 new Endereco(Guid.NewGuid(), "14800-0000", endereco.StreetAddress(false), string.Empty, endereco.City(), endereco.State(), "169958113859", "2005"));

            return faker.Generate();
        }

        public Endereco EnderecoMaxLength()
        {
            const string TEXTO_COM_MAIS_DE_250_CARACTERES = "AHIUDHASHOIFJOASJPFPOKAPFOKPKQPOFKOPQKWPOFEMMVIMWPOVPOQWPMVPMQOPIPQMJEOIPFMOIQOIFMCOKQMEWVMOPMQEOMVOPMWQOEMVOWMEOMVOIQMOIVMQEHISUAHDUIHASIUHDIHASIUHDUIHIAUSHIDUHAIUSDQWMFMPEQPOGFMPWEMGVWEM CQPWEM,CPQWPMCEOWIMVOEWOINMMFWOIEMFOIMOIOWEMFOIEWMFOIWEMFOWEOIMF";

            var endereco = new Faker("pt_BR").Address;

            var faker = new Faker<Endereco>("pt_BR");

            faker.CustomInstantiator(f =>
                 new Endereco(Guid.NewGuid(), "14800-000", TEXTO_COM_MAIS_DE_250_CARACTERES, TEXTO_COM_MAIS_DE_250_CARACTERES, TEXTO_COM_MAIS_DE_250_CARACTERES, endereco.StateAbbr(), "16995811385", "1234567"));

            return faker.Generate();
        }

    }
}
