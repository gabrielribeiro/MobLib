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
    public class PayUPlanService : MobService<Plan>, IPayUPlanService
    {
        private readonly PlanRestClient restClient;

        private readonly IPayUPlanIntervalService planIntervalService;

        private readonly IPayUAdditionalValueService additionalValueService;

        public PayUPlanService(IPayUPlanRepository repository,
            IPayUPlanIntervalService intervalService,
            IPayUAdditionalValueService additionalValueService,
            PlanRestClient restClient)
            : base(repository)
        {
            this.restClient = restClient;
            this.planIntervalService = intervalService;
            this.additionalValueService = additionalValueService;
            this.planIntervalService.AutoSaveEnabled = false;
            this.additionalValueService.AutoSaveEnabled = false;
        }

        public override void Insert(Plan plan)
        {
            if (plan == null)
            {
                throw new MobException("plano nao pode ser nulo");
            }

            if (string.IsNullOrEmpty(plan.Description))
            {
                throw new MobException("a descrição é obrigatória");
            }

            if (string.IsNullOrEmpty(plan.PlanCode))
            {
                throw new MobException("a codigo do plano é obrigatório");
            }

            if (this.Exists(x => x.PlanCode == plan.PlanCode))
            {
                throw new MobException("já existe um plano com este codigo");
            }

            if (string.IsNullOrEmpty(plan.AccountId))
            {
                plan.AccountId = Configuration.GetConfigurationValue("PayU_Api_Key");
            }

            var planInterval = this.planIntervalService.Find(plan.IntervalId);

            if (planInterval == null)
            {
                throw new MobException("intervalo nao encontrado");
            }

            plan.PlanInterval = planInterval;

            var postedPlan = this.restClient.Post(plan);

            if (postedPlan == null || string.IsNullOrEmpty(postedPlan.PlanPayUId))
            {
                throw new MobException("erro ao executar a solicitação AO PayU");
            }

            plan.PlanPayUId = postedPlan.PlanPayUId;

            base.Insert(plan);
        }

        public override void Update(Plan plan)
        {
            if (plan == null)
            {
                throw new MobException("plano nao pode ser nulo");
            }

            if (string.IsNullOrEmpty(plan.Description))
            {
                throw new MobException("a descrição é obrigatória");
            }

            if (string.IsNullOrEmpty(plan.PlanCode))
            {
                throw new MobException("a codigo do plano é obrigatório");
            }

            if (this.Exists(x => x.PlanCode == plan.PlanCode))
            {
                throw new MobException("já existe um plano com este codigo");
            }

            if (string.IsNullOrEmpty(plan.AccountId))
            {
                plan.AccountId = Configuration.GetConfigurationValue("PayU_Api_Key");
            }

            var planInterval = this.planIntervalService.Find(plan.IntervalId);

            if (planInterval == null)
            {
                throw new MobException("intervalo nao encontrado");
            }

            plan.PlanInterval = planInterval;

            var persistedPlan = this.Find(plan.Id);

            if (persistedPlan == null)
            {
                throw new MobException("plano nao encontrado");
            }

            if (persistedPlan.PlanCode != plan.PlanCode)
            {
                throw new MobException("o codigo do plano nao pode ser alterado");
            }

            persistedPlan.AccountId = plan.AccountId;
            persistedPlan.Description = plan.Description;
            persistedPlan.IntervalId = plan.IntervalId;
            persistedPlan.PlanInterval = plan.PlanInterval;
            persistedPlan.IntervalCount = plan.IntervalCount;
            persistedPlan.MaxPaymentsAllowed = plan.MaxPaymentsAllowed;
            persistedPlan.PaymentAttemptsDelay = plan.PaymentAttemptsDelay;
            persistedPlan.MaxPendingPayments = plan.MaxPendingPayments;
            persistedPlan.TrialDays = plan.TrialDays;

            
            foreach (var value in persistedPlan.AdditionalValues)
            {
                this.additionalValueService.Delete(value);
            }
            persistedPlan.AdditionalValues = null;
            
            this.additionalValueService.InsertRange(plan.AdditionalValues);

            var putedPlan = this.restClient.Put(persistedPlan);
            if (putedPlan == null)
            {
                throw new MobException("erro ao executar a solicitação ao PayU");
            }

            base.Update(plan);
        }

        public override void Delete(Plan plan)
        {
            if (plan == null)
            {
                throw new MobException("plano nao pode ser nulo");
            }

            if (this.restClient.Delete(plan.PlanCode)) 
            {
                base.Delete(plan);
            }
            else
            {
                throw new MobException("erro ao executar a solicitação ao PayU");
            }
        }

        public override void Update(Expression<Func<Plan, bool>> filterExpression, Expression<Func<Plan, Plan>> updateExpression)
        {
            throw new NotSupportedException("method not suported");
        }

        public override void InsertRange(IEnumerable<Plan> entities)
        {
            throw new NotSupportedException("method not suported");
        }

        public override void InsertRange(IEnumerable<Plan> entities, int batchSize)
        {
            throw new NotSupportedException("method not suported");
        }

        public override void Delete(Expression<Func<Plan, bool>> filterExpression)
        {
            throw new NotSupportedException("method not suported");
        }
    }
}
