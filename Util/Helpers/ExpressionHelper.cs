using System.Linq.Expressions;

namespace Util.Helpers
{
    public static class ExpressionHelper
    {
        public static string GetPropertyName<T>(Expression<Func<T, object>> expression)
        {
            if (expression.Body is UnaryExpression unaryExpression)
            {
                if (unaryExpression.Operand is MemberExpression memberExpression)
                {
                    return memberExpression.Member.Name;
                }
            }
            else if (expression.Body is MemberExpression member)
            {
                return member.Member.Name;
            }

            throw new ArgumentException("Invalid expression");
        }
    }

}
