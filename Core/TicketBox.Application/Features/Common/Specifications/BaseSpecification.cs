using System.Linq.Expressions;

namespace TicketBox.Application.Features.Common.Specifications
{
    public abstract class BaseSpecification<T> : ISpecification<T> //doğrudan nesne değil diğer sınıflar tarafından kullanılacak bir sınıf
    {
        public Expression<Func<T, bool>> Criteria { get; protected set; } //T tipinde bir nesne alır ve bool döndürür. Bu bir filtreleme kriteridir
                                                                          //Örn/ bir ürünün fiyatının belirli bir değerden büyük olup olmadığını kontrol etmek için kullanılabilir. 
        public List<Expression<Func<T, object>>> Includes { get; } = new(); //T tipinde bir nesne alır ve object döndürür. Bu, ilişkili verileri yüklemek için kullanılacak ifadeleri içerir.
                                                                            //Örn/ bir ürünün kategorisini veya tedarikçisini yüklemek için kullanılabilir.

        protected void AddCriteria(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria; //Criteria özelliğini ayarlar, filtreleme kriterini belirler.
        }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression); //Includes listesine bir ifade ekler, ilişkili verileri yüklemek için kullanılacak ifadeleri belirler.
        }
    }
}
