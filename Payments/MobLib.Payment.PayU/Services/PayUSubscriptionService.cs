﻿using MobLib.Core.Services;
using MobLib.Exceptions;
using MobLib.Payment.PayU.Domain.Contracts;
using MobLib.Payment.PayU.Domain.Entities;
using MobLib.Payment.PayU.Rest;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MobLib.Payment.PayU.Services
{
    public class PayUSubscriptionService : MobService<Subscription>, IPayUSubscriptionService
    {
        public readonly SubscriptionRestClient restClient;
        public readonly IPayUCustomerService customerService;
        public readonly IPayUCreditCardTokenService creditCardService;
        public readonly IPayUPlanService planService;

        public PayUSubscriptionService(
            SubscriptionRestClient restClient,
            IPayUCustomerService customerService,
            IPayUCreditCardTokenService creditCardService,
            IPayUPlanService planService,
            IPayUSubscriptionRepository repository
            ) : base(repository) 
        {
            this.restClient = restClient;
            this.customerService = customerService;
            this.creditCardService = creditCardService;
            this.planService = planService;
        }

        public override void Insert(Subscription subscription)
        {
            if (subscription == null)
            {
                throw new MobException("subscription nao pode ser nulo");
            }

            if (!this.customerService.Exists(x => x.Id == subscription.CustomerId))
            {
                this.customerService.Insert(subscription.Customer);
                subscription.CustomerId = subscription.Customer.Id;
            }
            else
            {
                subscription.Customer = this.customerService.Find(subscription.CustomerId);
            }

            if (!this.creditCardService.Exists(x => x.Id == subscription.CreditCardTokenId))
            {
                this.creditCardService.Insert(subscription.CreditCardToken);
                subscription.CreditCardTokenId = subscription.CreditCardToken.Id;
            }
            else
            {
                subscription.CreditCardToken = this.creditCardService.Find(subscription.CreditCardTokenId);
            }

            if (!this.planService.Exists(x => x.Id == subscription.PlanId))
            {
                throw new MobException("Plano inválido para inscrição");
            }
            else
            {
                subscription.Plan = this.planService.Find(subscription.PlanId);
            }

            var postedSubscription = this.restClient.Post(subscription);

            if (postedSubscription == null || string.IsNullOrEmpty(postedSubscription.SubscriptionPayUId))
            {
                throw new MobException("erro ao executar a solicitação ao PayU para inserir inscricao");
            }

            subscription.SubscriptionPayUId = postedSubscription.SubscriptionPayUId;
            subscription.StartPeriod = postedSubscription.StartPeriod;
            subscription.EndPeriod = postedSubscription.EndPeriod;

            base.Insert(subscription);
        }

        public override void Update(Subscription subscription)
        {
            if (subscription == null)
            {
                throw new MobException("subscription nao pode ser nulo");
            }


            if (!this.customerService.Exists(x => x.Id == subscription.CustomerId))
            {
                throw new MobException("Cliente inválido para inscrição");
            }
            else
            {
                subscription.Customer = this.customerService.Find(subscription.CustomerId);
            }

            if (!this.creditCardService.Exists(x => x.Id == subscription.CreditCardTokenId))
            {
                throw new MobException("Cliente inválido para inscrição");
            }
            else
            {
                subscription.CreditCardToken = this.creditCardService.Find(subscription.CreditCardTokenId);
            }

            if (!this.planService.Exists(x => x.Id == subscription.PlanId))
            {
                throw new MobException("Cliente inválido para inscrição");
            }
            else
            {
                subscription.Plan = this.planService.Find(subscription.PlanId);
            }

            var persistedSubscription = this.Find(subscription.Id);

            persistedSubscription.CustomerId = subscription.CustomerId;
            persistedSubscription.Customer = subscription.Customer;
            persistedSubscription.Plan = subscription.Plan;
            persistedSubscription.PlanId = subscription.PlanId;
            persistedSubscription.CreditCardToken = subscription.CreditCardToken;
            persistedSubscription.CreditCardTokenId = subscription.CreditCardTokenId;

            var putedSubscription = this.restClient.Put(persistedSubscription);
            if (putedSubscription == null)
            {
                throw new MobException("erro ao executar a solicitação ao PayU para atualizar inscricao");
            }

            base.Update(subscription);
        }

        public override void Delete(Subscription subscription)
        {
            if (subscription == null)
            {
                throw new MobException("subscription nao pode ser nulo");
            }

            subscription = this.Find(subscription.Id);

            if (subscription == null)
            {
                throw new MobException("subscription nao pode ser nulo");
            }

            if (this.restClient.Delete(subscription.SubscriptionPayUId))
            {
                base.Delete(subscription);
            }
            else
            {
                throw new MobException("erro ao executar a solicitação ao PayU para excluir inscricao");
            }
        }

        public override void Update(Expression<Func<Subscription, bool>> filterExpression, Expression<Func<Subscription, Subscription>> updateExpression)
        {
            throw new NotSupportedException("method not suported");
        }

        public override void InsertRange(IEnumerable<Subscription> entities)
        {
            throw new NotSupportedException("method not suported");
        }

        public override void InsertRange(IEnumerable<Subscription> entities, int batchSize)
        {
            throw new NotSupportedException("method not suported");
        }

        public override void Delete(Expression<Func<Subscription, bool>> filterExpression)
        {
            throw new NotSupportedException("method not suported");
        }
    }
}
