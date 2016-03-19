using MobLib.Core.Services;
using MobLib.Exceptions;
using MobLib.Payment.PayU.Domain.Contracts;
using MobLib.Payment.PayU.Domain.Entities;
using MobLib.Payment.PayU.Rest;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MobLib.Payment.PayU.Services
{
    public class PayUCustomerService : MobService<Customer>, IPayUCustomerService
    {
        private readonly CustomerRestClient restClient;
        public PayUCustomerService(IPayUCustomerRepository repository,
            CustomerRestClient restClient)
            : base(repository)
        {
            this.restClient = restClient;
        }

        public override void Insert(Customer customer)
        {
            if (customer == null)
            {
                throw new MobException("customer nao pode ser nulo");
            }

            var postedCustomer = this.restClient.Post(customer);

            if (postedCustomer == null || string.IsNullOrEmpty(postedCustomer.CustomerPayUId))
            {
                throw new MobException("erro ao executar  a solicitação ao PayU para inserir cliente");
            }

            customer.CustomerPayUId = postedCustomer.CustomerPayUId;

            base.Insert(customer);
        }

        public override void Update(Customer customer)
        {
            if (customer == null)
            {
                throw new MobException("customer nao pode ser nulo");
            }

            var persistedCustomer = this.Find(customer.Id);

            persistedCustomer.FullName = customer.FullName;
            persistedCustomer.EmailAddress = customer.EmailAddress;
            persistedCustomer.ContactPhone = customer.ContactPhone;

            var putedCustomer = this.restClient.Put(persistedCustomer);
            if (putedCustomer == null)
            {
                throw new MobException("erro ao executar a solicitação ao PayU para atualizar cliente");
            }

            base.Update(customer);
        }

        public override void Delete(Customer customer)
        {
            if (customer == null)
            {
                throw new MobException("customer nao pode ser nulo");
            }

            customer = this.Find(customer.Id);

            if (customer == null)
            {
                throw new MobException("customer nao pode ser nulo");
            }

            if (this.restClient.Delete(customer.CustomerPayUId))
            {
                base.Delete(customer);
            }
            else
            {
                throw new MobException("erro ao executar a solicitação ao PayU para excluir cliente");
            }
        }

        public override void Update(Expression<Func<Customer, bool>> filterExpression, Expression<Func<Customer, Customer>> updateExpression)
        {
            throw new NotSupportedException("method not suported");
        }

        public override void InsertRange(IEnumerable<Customer> entities)
        {
            throw new NotSupportedException("method not suported");
        }

        public override void InsertRange(IEnumerable<Customer> entities, int batchSize)
        {
            throw new NotSupportedException("method not suported");
        }

        public override void Delete(Expression<Func<Customer, bool>> filterExpression)
        {
            throw new NotSupportedException("method not suported");
        }
    }
}
