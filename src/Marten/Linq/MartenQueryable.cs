﻿using System.Linq;
using System.Linq.Expressions;
using Npgsql;
using Remotion.Linq;
using Remotion.Linq.Parsing.Structure;

namespace Marten.Linq
{
    public class MartenQueryable<T> : QueryableBase<T>
    {
        public MartenQueryable(IQueryParser queryParser, IDocumentExecutor executor) : base(queryParser, executor)
        {

        }

        public MartenQueryable(IQueryProvider provider) : base(provider)
        {
        }

        public MartenQueryable(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }
    }
}