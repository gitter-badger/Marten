using System;
using System.Collections.Concurrent;
using FubuCore;
using Marten.Generation;

namespace Marten.Schema
{
    public class DevelopmentDocumentSchema : IDocumentSchema, IDisposable
    {
        private readonly Lazy<CommandRunner> _runner;
        private readonly ConcurrentDictionary<Type, IDocumentStorage> _documentTypes = new ConcurrentDictionary<Type, IDocumentStorage>(); 

        public DevelopmentDocumentSchema(IConnectionFactory connections)
        {
            _runner = new Lazy<CommandRunner>(() => new CommandRunner(connections.Create()));
        }

        public IDocumentStorage StorageFor(Type documentType)
        {
            return _documentTypes.GetOrAdd(documentType, type =>
            {
                // TODO -- will need to get fancier later when we stop requiring IDocument
                var storage = typeof (DocumentStorage<>).CloseAndBuildAs<IDocumentStorage>(documentType);

                var builder = new SchemaBuilder();
                storage.InitializeSchema(builder);

                _runner.Value.Execute(builder.ToSql());

                return storage;
            });
        }

        public void Dispose()
        {
            if (_runner.IsValueCreated) _runner.Value.Dispose();
        }
    }
}