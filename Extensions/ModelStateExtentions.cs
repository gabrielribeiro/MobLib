using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Http = System.Web.Http.ModelBinding;

namespace MobLib.Extensions
{
    public static class ModelStateExtentions
    {
        public static void RemoveFor<TModel>(this Http.ModelStateDictionary modelState,
                                         Expression<Func<TModel, object>> expression)
        {
            string expressionText = ExpressionHelper.GetExpressionText(expression);

            foreach (var ms in modelState.ToArray())
            {
                if (ms.Key.Contains( "."+ expressionText ))
                {
                    modelState.Remove(ms);
                }
            }
        }
    }
}
