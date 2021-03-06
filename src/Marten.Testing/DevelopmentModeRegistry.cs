using Marten.Linq;
using Marten.Schema;
using Remotion.Linq.Parsing.Structure;
using StructureMap;

namespace Marten.Testing
{
    public class DevelopmentModeRegistry : Registry
    {
        public DevelopmentModeRegistry()
        {
            For<IConnectionFactory>().Use<ConnectionSource>();
            ForSingletonOf<IDocumentSchema>().Use<DevelopmentDocumentSchema>();
            For<IDocumentSession>().Use<DocumentSession>();
            For<ISerializer>().Use<JsonNetSerializer>();

            ForSingletonOf<IQueryParser>().Use<MartenQueryParser>();
        }
    }
}