using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API.Core.Annotations;
using MongoDB.Bson;
using MongoDB.Driver;

namespace API.Core
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection; 

        public MongoRepository(IMongoDbSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute) documentType.GetCustomAttributes(
                typeof(BsonCollectionAttribute),
                true)
            .FirstOrDefault())?.CollectionName;
        }       

        public virtual IQueryable<TDocument> AsQueryable()
        {
            return _collection.AsQueryable();
        }

        public virtual IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).ToEnumerable(); 
        }

        public virtual IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<TDocument, bool>> filterExpression, Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        //Get All Data From Collection
        public virtual IEnumerable<TDocument> GetAll() => 
            _collection.Find(TDocument => true).ToList();
        
        //Get All Data From Collection Using Async
        public virtual async Task<IEnumerable<TDocument>> GetAllAsync()
        {
            try
            {
                return await _collection.Find(_ => true).ToListAsync();
            } 
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public virtual TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).FirstOrDefault();
        }

        public virtual Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
        }

        //Get Data By Id From Collection
        public virtual TDocument FindById(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(fil => fil.Id, objectId);
            return _collection.Find(filter).SingleOrDefault();
        }

        //Get Data By Id From Collection Using Async
        public virtual Task<TDocument> FindByIdAsync(string id)
        {
            return Task.Run(() => 
            {
                var objectId = new ObjectId(id);
                var filter = Builders<TDocument>.Filter.Eq(fil => fil.Id, objectId);
                return _collection.Find(filter).SingleOrDefaultAsync();
            });
        }

        //Insert One Data To Collection
        public virtual void InsertOne(TDocument document)
        {
            _collection.InsertOne(document);
        }

        //Insert One Data To Collection Using Async
        public virtual Task InsertOneAsync(TDocument document)
        {
            return Task.Run(() => _collection.InsertOneAsync(document));
        }

        //Insert One Or More Data To Collection
        public virtual void InsertMany(ICollection<TDocument> documents)
        {
            _collection.InsertMany(documents);
        }

         //Insert One Or More Data To Collection Using Async
        public virtual Task InsertManyAsync(ICollection<TDocument> documents)
        {
            return Task.Run(() => _collection.InsertManyAsync(documents));
        }

        public void ReplaceOne(string id, TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id.ToString(), id);
            _collection.FindOneAndReplace(filter, document);
        }

        public virtual async Task ReplaceOneAsync(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(fil => fil.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(filter, document);
        }

        public void DeleteOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            _collection.FindOneAndDelete(filterExpression);
        }

        public Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));
        }

        public void DeleteById(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(fil => fil.Id, objectId);
            _collection.FindOneAndDelete(filter);
        }

        public Task DeleteByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
                _collection.FindOneAndDeleteAsync(filter);
            });
        }

        public void DeleteMany(Expression<Func<TDocument, bool>> filterExpression)
        {
            _collection.DeleteMany(filterExpression);
        }

        public Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.DeleteManyAsync(filterExpression));
        }
    }
}