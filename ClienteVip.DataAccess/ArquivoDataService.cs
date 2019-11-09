using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClienteVip.Model;
using Newtonsoft.Json;

namespace ClienteVip.DataAccess
{
    public class ArquivoDataService : IDataService
    {
        private const string ArquivoJSON = "Cliente.json";

        public void Dispose()
        {

        }

        public void ExcluirCliente(int clienteId)
        {
            var cliente = LerAquivoJSON();
            var exite = cliente.Single(cli => cli.Id == clienteId);
            cliente.Remove(exite);
            SalvarEmArquivo(cliente);


        }

        public IEnumerable<Cliente> ObtemTodosClientes()
        {
            return LerAquivoJSON();
        }

        public Cliente ObterClienteById(int clienteId)
        {
            var cliente = LerAquivoJSON();
            return cliente.Single(cli => cli.Id == clienteId);
        }

        public IEnumerable<GrupoCliente> ObterTodosGrupoCliente()
        {
            yield return new GrupoCliente { Id = 1, Nome = "NOVO" };
            yield return new GrupoCliente { Id = 2, Nome = "EMINENTE" };
            yield return new GrupoCliente { Id = 3, Nome = "NORMAL" };
            yield return new GrupoCliente { Id = 4, Nome = "POTENCIAL" };

            yield return new GrupoCliente { Id = 5, Nome = "POTENCIAL1" };
            yield return new GrupoCliente { Id = 6, Nome = "POTENCIAL2" };
            yield return new GrupoCliente { Id = 7, Nome = "POTENCIAL3" };
            yield return new GrupoCliente { Id = 8, Nome = "POTENCIAL4" };
            yield return new GrupoCliente { Id = 9, Nome = "POTENCIAL5" };
            yield return new GrupoCliente { Id = 10, Nome = "POTENCIAL6" };

        }

        public void SalvarCliente(Cliente cliente)
        {
            if (cliente.Id <= 0)
            {
                InsereCliente(cliente);
            }
            else
            {
                AtualizaCliente(cliente);
            }
        }

        private void AtualizaCliente(Cliente cliente)
        {
            var clientes = LerAquivoJSON();
            var existe = clientes.Single(f => f.Id == cliente.Id);
            var indexOfExisting = clientes.IndexOf(existe);
            clientes.Insert(indexOfExisting, cliente);
            clientes.Remove(existe);
            SalvarEmArquivo(clientes);
        }

        private void InsereCliente(Cliente cliente)
        {
            var clientes = LerAquivoJSON();
            var maxClienteId = clientes.Max(f => f.Id);
            cliente.Id = maxClienteId + 1;
            cliente.Id = maxClienteId + 1;
            SalvarEmArquivo(clientes);
        }

        private void SalvarEmArquivo(List<Cliente> clienteList)
        {
            string json = JsonConvert.SerializeObject(clienteList, Formatting.Indented);
            File.WriteAllText(ArquivoJSON, json);
        }

        private List<Cliente> LerAquivoJSON()
        {
            if (!File.Exists(ArquivoJSON))
            {
                return new List<Cliente>
                {
                    new Cliente{Id=1,Nome = "MARCELLO",Sobrenome="AZEVEDO",Endereco=new Endereco{CEP="11223322",Rua="ASDASDASD",Numero = "12345",Bairro = "12345",Complemento = "ASDASDASDASD"},
                        DataNascimento = new DateTime(1980,11,30), Vip = true,Emails=new List<EmailCliente>{new EmailCliente { Email="ASDASDASDASD@ASFDASDASD.com"}},GrupoClienteId = 1} };
            };

            string json = File.ReadAllText(ArquivoJSON);
            return JsonConvert.DeserializeObject<List<Cliente>>(json);

        }
    }
}
