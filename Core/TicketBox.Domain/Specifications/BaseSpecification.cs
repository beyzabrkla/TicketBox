using System.Linq.Expressions;

namespace TicketBox.Domain.Specifications
    {
    public abstract class BaseSpecification<T>:ISpecification<T> //doğrudan nesne değil diğer sınıflar tarafından kullanılacak bir sınıf
    {
        public Expression<Func<T, bool>> Criteria { get; protected set; } //T tipinde bir nesne alır ve bool döndürür. Bu bir filtreleme kriteridir
                                                                          //Örn/ bir ürünün fiyatının belirli bir değerden büyük olup olmadığını kontrol etmek için kullanılabilir. 
        public List<Expression<Func<T, object>>> Includes { get; } = new(); //T tipinde bir nesne alır ve object döndürür. Bu, ilişkili verileri yüklemek için kullanılacak ifadeleri içerir.
                                                                            // Sayfalama için yeni özellikler
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; }

        private Expression<Func<T, object>> _orderBy;
        private Expression<Func<T, object>> _orderByDescending;

        public Expression<Func<T, object>> OrderBy => _orderBy;
        public Expression<Func<T, object>> OrderByDescending => _orderByDescending;

        protected void AddCriteria(Expression<Func<T, bool>> criteria) => Criteria = criteria;
        protected void AddInclude(Expression<Func<T, object>> includeExpression) => Includes.Add(includeExpression);

        // SAYFALAMA METODU BURAYA EKLENECEK
        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

        protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression) => _orderBy = orderByExpression;
        protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression) => _orderByDescending = orderByDescendingExpression;
    }
}
